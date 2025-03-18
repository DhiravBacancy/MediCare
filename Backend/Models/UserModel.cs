using System;

namespace MediCare.Models
{
    public class User
    {
        public int UserId { get; set; }

        // Basic user information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public DateTime? DateOfRelieving { get; set; }
        public string MobileNo { get; set; }
        public string EmergencyNo { get; set; }

        // Credentials and role information
        public string Password { get; set; }
        public int RoleId { get; set; }  // FK to Role

        // Navigation property (marked as virtual)
        public virtual Role Role { get; set; }

        // Status and activity
        public bool Active { get; set; }

        // Audit fields
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Refresh Token functionality
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
