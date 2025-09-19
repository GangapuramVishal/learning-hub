using EmployeesDataBase.Interfaces;
using EmployeesDataBase.Models;
using EmployeesDeskAPI.Caching;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EmployeesDeskAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //All method's are in interface so to use in controller we declare this
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICacheProvider _cacheProvider;
        public EmployeeController(IEmployeeRepository employeeRepository, ICacheProvider cacheProvider)
        {
            _employeeRepository = employeeRepository;
            _cacheProvider = cacheProvider;
        }

        [HttpGet]
        public ActionResult GetEmployees()
        {
            List<Employee> employees;
            string source;

            //for the 1st call this statement is true or // Check if data is in cache
            //TryGetValue checks if the list of employees is already cached or not
            if (!_cacheProvider.TryGetValue(Cachekeys.Employee,out employees))
            {
                // Data not found in cache, retrieve from database
                employees = _employeeRepository.GetEmployees();
                // Cache the data for future requests
                var cacheEntryOption = new MemoryCacheEntryOptions //you can control various aspects of how cached items are managed, expired, and evicted from the in-memory cache.
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(15),
                    Size = 1024
                };
                _cacheProvider.Set(Cachekeys.Employee, employees, cacheEntryOption);
                // Indicate that data is retrieved from the database
                source = "Database";
            }
            else
            {
                // Indicate that data is retrieved from cache
                source = "InMemory Cache";
                // Add X-Cache header to indicate data retrieved from cache
                //Response.Headers.Add("X-Cache", "Data retrieved from cache");
            }

            // Set the custom header indicating the source of the data
            Response.Headers.Add("X-Data-Source", source);

            return Ok(employees);

            //Code without using Caching
            //try
            //{
            //    var employees = _employeeRepository.GetEmployees();
            //    return Ok(employees);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(StatusCodes.Status404NotFound);
            //}
        }

        [HttpGet]
        public ActionResult GetEmployeeById(int id) 
        {
            try
            {
                var employee = _employeeRepository.GetEmployeeById(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);  
            }
        }
        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            try
            {
                var addedEmployee = _employeeRepository.AddEmployee(employee);
                return Ok(addedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
        }
    }
}


/*IMemoryCache is a specific interface for working with in-memory caching provided by ASP.NET Core,
 * while ICacheProvider is a custom interface that abstracts caching functionality in a generic way,
 * allowing for flexibility in choosing and swapping caching implementations. Depending on 
 * your application's requirements, you may use one or both of these interfaces.
 */






