using CascadeCache.Models;
using Microsoft.Extensions.Caching.Memory;

namespace CascadeCache.Handlers
{
    public class CacheEmployeeHandler : EmployeeHandler
    {
        private readonly IMemoryCache _cache;

        public CacheEmployeeHandler(IMemoryCache cache)
        {
            _cache = cache;
        }

        public override async Task<Employee> HandleAsync(int employeeId)
        {
            if (_cache.TryGetValue(employeeId, out Employee cachedEmployee))
            {
                return cachedEmployee;
            }

            return await NextHandler?.HandleAsync(employeeId);
        }
    }

}
