using CascadeCache.Interfaces;
using CascadeCache.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CascadeCache.Handlers
{
    public class ExternalApiEmployeeHandler : EmployeeHandler
    {
        private readonly IExternalApiService _externalApiService;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMemoryCache _cache;

        public ExternalApiEmployeeHandler(IExternalApiService externalApiService, ApplicationDbContext dbContext, IMemoryCache cache)
        {
            _externalApiService = externalApiService;
            _dbContext = dbContext;
            _cache = cache;
        }

        public override async Task<Employee> HandleAsync(int employeeId)
        {
            var employee = await _externalApiService.GetEmployeeByIdAsync(employeeId);
            if (employee != null)
            {
                _dbContext.Employees.Add(employee);
                await _dbContext.SaveChangesAsync();
                _cache.Set(employeeId, employee); 
            }

            return employee;
        }
    }
}
