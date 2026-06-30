using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using University.API.Filters;
using University.API.Helpers;
using University.Core.Forms;
using University.Core.Services;

namespace University.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenHelper _jwtTokenService;
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IJwtTokenHelper jwtTokenHelper,
            IAuthService authService,
            ILogger<AuthController> logger)
        {
            _jwtTokenService = jwtTokenHelper;
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ApiResponse> Register([FromBody] RegisterForm form)
        {
            var dto = await _authService.Register(form);
            return new ApiResponse("User registered successfully", dto);
        }

        [HttpPost("login")]
        public async Task<ApiResponse> Login([FromBody] LoginForm form)
        {
            var user = await _authService.Login(form);
            var token = _jwtTokenService.GenerateToken(user);
            return new ApiResponse("Login successful", token);
        }

        [Authorize]
        [HttpGet("me")]
        public ApiResponse Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var email = User.FindFirstValue(ClaimTypes.Email);

            _logger.LogInformation("Current user - Id: {UserId}, Role: {Role}", userId, role);

            return new ApiResponse(new
            {
                Id = userId,
                Email = email,
                Role = role
            });
        }
    }
}
