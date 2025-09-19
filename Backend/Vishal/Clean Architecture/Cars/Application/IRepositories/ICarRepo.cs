using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface ICarRepo
    {
        Task AddNewAsync(Car car);
        Task<List<Car>> GetAllAsync();
        Task <Car> GetByIdAsync(int id);
        Task UpdateAsync(Car car);
        Task DeleteAsync(Car car);
    }
}
