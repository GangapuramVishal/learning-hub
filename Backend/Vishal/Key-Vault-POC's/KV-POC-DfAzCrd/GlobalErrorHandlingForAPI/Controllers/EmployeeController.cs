using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GlobalErrorHandlingForAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesAsync()
        {
            // Retrieve all employees from the database asynchronously
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployeeAsync(Employee employee)
        {
            // Add a new employee to the database asynchronously
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEmployeeByIdAsync), new { id = employee.Id }, employee);
        }

        // GET: api/Employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeByIdAsync(int id)
        {
            // Retrieve an employee by ID from the database asynchronously
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
