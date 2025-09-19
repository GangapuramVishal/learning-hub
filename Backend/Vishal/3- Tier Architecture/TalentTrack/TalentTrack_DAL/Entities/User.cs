using System.ComponentModel.DataAnnotations;

namespace TalentTrack_DAL.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public required string EmployeeID { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }

        public required int PhoneNo { get; set; }

        public required string PersonalEmail { get; set; }

        public required string Designation { get; set; }

        public int? ManagerID { get; set; }

        public User? Manager { get; set; }

        public int ExperienceYears { get; set; }

        public string AvailabilityStatus { get; set; } = "Available";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
