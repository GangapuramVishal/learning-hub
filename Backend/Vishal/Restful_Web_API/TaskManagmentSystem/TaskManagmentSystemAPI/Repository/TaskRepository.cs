using TaskManagmentSystemAPI.Data;
using TaskManagmentSystemAPI.Models;
using TaskManagmentSystemAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace TaskManagmentSystemAPI.Repository
{
    public class TaskRepository :Repository<TaskModel>, ITaskRepository
    {
        private readonly ApplicationDbContext _db;
        public TaskRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public async Task<TaskModel> UpdateAsync(TaskModel entity)
        //{
        //    // Retrieve the existing record from the database
        //    var existingEntity = await _db.Tasks.FindAsync(entity.Id);
        //    if (existingEntity == null)
        //    {
        //        throw new InvalidOperationException("Entity not found");
        //    }

        //    // Preserve the CreatedDate value
        //    entity.CreatedDate = existingEntity.CreatedDate;

        //    // Update other fields and set UpdatedDate
        //    entity.UpdatedDate = DateTime.Now;
        //    _db.Entry(existingEntity).CurrentValues.SetValues(entity);

        //    await _db.SaveChangesAsync();
        //    return entity;
        //}

        public async Task<TaskModel> UpdateAsync(TaskModel entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Tasks.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
    
}
