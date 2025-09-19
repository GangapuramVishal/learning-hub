using CascadeCache.Interfaces;
using CascadeCache.Models;

namespace CascadeCache.Services
{
    public class GetEmployeeByIdService : IEmployeeService
    {
        private readonly IEmployeeHandler _firstHandler;

        public GetEmployeeByIdService(IEmployeeHandler firstHandler)
        {
            _firstHandler = firstHandler;
        }

        public async Task<Employee> GetByIdAsync(int employeeId)
        {
            return await _firstHandler.HandleAsync(employeeId);
        }
    }
}
