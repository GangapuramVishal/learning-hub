using CascadeCache.Models;

namespace CascadeCache.Interfaces
{
    public interface IExternalApiService
    {
        Task<Employee> GetEmployeeByIdAsync(int id);
    }
}
