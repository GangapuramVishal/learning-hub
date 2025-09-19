using CascadeCache.Models;

namespace CascadeCache.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetByIdAsync(int employeeId);
    }
}