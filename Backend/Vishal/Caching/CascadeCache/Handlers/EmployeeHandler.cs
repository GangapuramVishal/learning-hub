using CascadeCache.Interfaces;
using CascadeCache.Models;

namespace CascadeCache.Handlers
{
    public abstract class EmployeeHandler : IEmployeeHandler
    {
        protected IEmployeeHandler NextHandler { get; private set; }

        public void SetNextHandler(IEmployeeHandler nextHandler)
        {
            NextHandler = nextHandler;
        }

        public abstract Task<Employee> HandleAsync(int employeeId);
    }
}
