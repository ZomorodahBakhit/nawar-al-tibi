namespace University.Core.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmed { get; set; }
        public string Phone { get; set; } = string.Empty;
        public bool PhoneNumberConfirmed { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
