using EmployeesDataBase.DatabaseContext;
using EmployeesDataBase.Interfaces;
using EmployeesDataBase.Models;

namespace EmployeesDataBase.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        //to manupulate data we have to use DbContext class so inject in ctor
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        //implement methods created in IEmployeeRepository
        public List<Employee> GetEmployees()
        {
            var listEmployees = _employeeDbContext.Employees.ToList();
            return listEmployees;
        }
        public Employee GetEmployeeById(int id)
        {
            var employee = _employeeDbContext.Employees.FirstOrDefault(x => x.EmployeeId == id);
            return employee;
        }
        public Employee AddEmployee(Employee employee)
        {
            _employeeDbContext.Employees.Add(employee);
            _employeeDbContext.SaveChanges();
            return employee;
        }
    }
}
