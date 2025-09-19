using CascadeCache.Models;

namespace CascadeCache.Repository
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(int id);
    }
}
