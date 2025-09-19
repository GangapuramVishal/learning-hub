using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SampleDataAccess
    {
        private readonly IMemoryCache _memoryCache;

        public SampleDataAccess(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public List<EmployeeModel> GetEmployees()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Jim", LastName = "Jones" });
            output.Add(new() { FirstName = "Alis", LastName = "Storm" });
            output.Add(new() { FirstName = "Stark", LastName = "Tony" });

            Thread.Sleep(3000);

            return output;
        }
        public async Task<List<EmployeeModel>> GetEmployeesAsync()
        {
            List<EmployeeModel> output = new();

            output.Add(new() { FirstName = "Jim", LastName = "Jones" });
            output.Add(new() { FirstName = "Alis", LastName = "Storm" });
            output.Add(new() { FirstName = "Stark", LastName = "Tony" });

            await Task.Delay(3000);

            return output;
        }

        public async Task<List<EmployeeModel>> GetEmployeesCache()
        {
            List<EmployeeModel> output;
            //get the cache value 
            output = _memoryCache.Get<List<EmployeeModel>>(key: "employees");
            if (output is null)                    //If the value is not found in the cache (i.e., null), it creates a new list of employees
            {
                output = new();

                output.Add(new() { FirstName = "Jim", LastName = "Jones" });
                output.Add(new() { FirstName = "Alis", LastName = "Storm" });
                output.Add(new() { FirstName = "Stark", LastName = "Tony" });

                await Task.Delay(3000);
                _memoryCache.Set(key: "employees", output, TimeSpan.FromMinutes(value: 1)); //After creating the list, it adds the list to the cache 
            }
            return output;
        }
    }
}
