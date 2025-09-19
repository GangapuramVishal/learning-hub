using TaskManagmentSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//using System.Xml.Linq;

namespace TaskManagmentSystemAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        public DbSet<TaskModel> Tasks { get; set; }  //table name in SQL

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>().HasData(
                new TaskModel
                {
                    Id = 1,
                    Title = "Python",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    CreatedDate = DateTime.Now,
                    Priority = "Low",
                    Status = "InProgress"
                },
                new TaskModel
                {
                    Id = 2,
                    Title = "JS",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    CreatedDate = DateTime.Now,
                    Priority = "Medium",
                    Status = "ToDo"
                },
                new TaskModel
                {
                    Id = 3,
                    Title = "C#",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    CreatedDate = DateTime.Now,
                    Priority = "High",
                    Status = "InProgress"
                },
                new TaskModel
                {
                    Id = 4,
                    Title = "HTML",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    CreatedDate = DateTime.Now,
                    Priority = "High",
                    Status = "Done"
                },
                new TaskModel
                {
                    Id = 5,
                    Title = "CSS",
                    Description = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                    CreatedDate = DateTime.Now,
                    Priority = "High",
                    Status = "InProgress"
                });
        }
    }
}
