using CascadeCache.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CascadeCache.Handlers
{
    public class DatabaseEmployeeHandler : EmployeeHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMemoryCache _cache;

        public DatabaseEmployeeHandler(ApplicationDbContext dbContext, IMemoryCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public override async Task<Employee> HandleAsync(int employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);
            if (employee != null)
            {
                _cache.Set(employeeId, employee);
                return employee;
            }

            return await NextHandler?.HandleAsync(employeeId);
        }
    }

}
