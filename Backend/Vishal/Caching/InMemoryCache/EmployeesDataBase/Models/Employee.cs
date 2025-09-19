namespace EmployeesDataBase.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? City { get; set; }
        public DateTime DateOfJoining { get; set; } = DateTime.Now;
        public float Salary { get; set; }
    }
}
