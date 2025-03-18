namespace MediCare.DTOs
{
    public class UserDTOs
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNo { get; set; }
        public string EmergencyNo { get; set; }
        public int RoleId { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; } // Ensure this field is included
    }

    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }
    }
}
