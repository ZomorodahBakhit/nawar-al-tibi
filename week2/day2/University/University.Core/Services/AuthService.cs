using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
using University.Data.Entities;

namespace University.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            ILogger<AuthService> logger,
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<UserDTO> Register(RegisterForm form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var validation = FormValidator.Validate(form);
            if (!validation.IsValid)
                throw new BusinessException(validation.Errors);

            var userExists = await _userManager.FindByEmailAsync(form.Email);
            if (userExists != null)
                throw new BusinessException("User already exists with this email.");

            if (!await _roleManager.RoleExistsAsync(form.Role))
                throw new BusinessException($"Role '{form.Role}' does not exist.");

            var user = new User
            {
                Email = form.Email,
                UserName = form.Email,
                FirstName = form.FirstName,
                LastName = form.LastName
            };

            var result = await _userManager.CreateAsync(user, form.Password);
            if (!result.Succeeded)
            {
                throw new BusinessException(result.Errors
                    .GroupBy(x => x.Code)
                    .ToDictionary(x => x.Key, y => y.Select(a => a.Description).ToList()));
            }

            await _userManager.AddToRoleAsync(user, form.Role);

            _logger.LogInformation("User {Email} registered with role {Role}", form.Email, form.Role);

            return new UserDTO
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email ?? string.Empty,
                EmailConfirmed = user.EmailConfirmed,
                Phone = user.PhoneNumber ?? string.Empty,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                Role = form.Role
            };
        }

        public async Task<UserDTO> Login(LoginForm form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            var validation = FormValidator.Validate(form);
            if (!validation.IsValid)
                throw new BusinessException(validation.Errors);

            var result = await _signInManager.PasswordSignInAsync(
                form.Email,
                form.Password,
                form.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(form.Email);
                if (user == null)
                    throw new NotFoundException($"Unable to find account with email {form.Email}.");

                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault() ?? "Student";

                return new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email ?? string.Empty,
                    EmailConfirmed = user.EmailConfirmed,
                    Phone = user.PhoneNumber ?? string.Empty,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    Role = role
                };
            }

            if (result.IsLockedOut)
                throw new BusinessException("Account is locked out.");

            if (result.IsNotAllowed)
                throw new BusinessException("Account is not allowed to login.");

            throw new BusinessException("Invalid login attempt.");
        }
    }

    public interface IAuthService
    {
        Task<UserDTO> Login(LoginForm request);
        Task<UserDTO> Register(RegisterForm form);
    }
}
