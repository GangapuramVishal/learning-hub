using EmployeesDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesDataBase.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        Employee AddEmployee(Employee employee);
    }
}
