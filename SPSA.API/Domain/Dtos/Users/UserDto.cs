namespace SPSA.API.Domain.Dtos.Users
{
    public class UserDto
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; } 
        public string RoleName { get; set; }
        public string Email { get; set; } 
    }
}
