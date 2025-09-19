using CascadeCache.Models;

namespace CascadeCache.Interfaces
{
    public interface IEmployeeHandler
    {
        Task<Employee> HandleAsync(int employeeId);
    }
}
