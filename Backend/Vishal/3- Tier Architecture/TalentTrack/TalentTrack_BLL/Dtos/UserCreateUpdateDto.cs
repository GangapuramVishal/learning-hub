using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalentTrack_BLL.Dtos
{
    public class UserCreateUpdateDto
    {
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNo { get; set; }
        public string? PersonalEmail { get; set; }
        public string Designation { get; set; }
        public int? ManagerID { get; set; }
        public int ExperienceYears { get; set; }
        public string AvailabilityStatus { get; set; }
    }
}
