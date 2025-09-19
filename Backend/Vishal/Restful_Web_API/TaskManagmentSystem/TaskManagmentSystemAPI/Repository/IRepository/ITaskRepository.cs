using TaskManagmentSystemAPI.Models;
using System.Linq.Expressions;

namespace TaskManagmentSystemAPI.Repository.IRepository
{
    public interface ITaskRepository : IRepository<TaskModel>
    {
        Task<TaskModel> UpdateAsync(TaskModel entity);
    }
    
}
