using Application.IRepository;
using Application.Models;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands
{
    public class UpdateCarRequest : IRequest<bool>
    {
        public UpdateCar UpdateCar { get; set; }

        public UpdateCarRequest(UpdateCar updateCar)
        {
            UpdateCar = updateCar;
        }
    }

    public class UpdateCarRequestHandler : IRequestHandler<UpdateCarRequest, bool>
    {
        private readonly ICarRepo _carRepo;

        public UpdateCarRequestHandler(ICarRepo carRepo)
        {
            _carRepo = carRepo;
        }

        public async Task<bool> Handle(UpdateCarRequest request, CancellationToken cancellationToken)
        {
            //Check if data exists in Db
            Car carInDb = await _carRepo.GetByIdAsync(request.UpdateCar.Id);
            if(carInDb != null)
            {
                carInDb.Brand = request.UpdateCar.Brand;
                carInDb.Model = request.UpdateCar.Model;
                carInDb.Description = request.UpdateCar.Description;
                carInDb.Type = request.UpdateCar.Type;
                carInDb.Color = request.UpdateCar.Color;
                carInDb.Year = request.UpdateCar.Year;
                carInDb.Price = request.UpdateCar.Price;
                carInDb.Mileage = request.UpdateCar.Mileage;
                carInDb.Transmission = request.UpdateCar.Transmission;
                carInDb.Seats = request.UpdateCar.Seats;
                carInDb.FuelType = request.UpdateCar.FuelType;
                carInDb.EngineSize = request.UpdateCar.EngineSize;
                carInDb.HasSunroof = request.UpdateCar.HasSunroof;


                await _carRepo.UpdateAsync(carInDb);
                return true;
            }
            return false;
        }
    }
}
