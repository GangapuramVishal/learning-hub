using Application.IRepository;
using Domain;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    //This class is responsible for providing Implemention of ICarRepo
    public class CarRepo : ICarRepo
    {
        //To get access for database 
        private readonly ApplicationDbContext _context;

        public CarRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddNewAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Car car)
        {
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _context.Cars
                .ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Car car)
        {
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
    }
}
