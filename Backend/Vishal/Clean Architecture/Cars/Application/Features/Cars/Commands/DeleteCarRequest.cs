using Application.IRepository;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Commands
{
    public class DeleteCarRequest :IRequest<bool>
    {
        public int CarId { get; set; }
        public DeleteCarRequest(int carId)
        {
            CarId = carId;
        }
    }

    public class DeleteCarRequestHandler : IRequestHandler<DeleteCarRequest, bool>
    {
        private readonly ICarRepo _carRepo;

        public DeleteCarRequestHandler(ICarRepo carRepo)
        {
            _carRepo = carRepo;
        }

        public async Task<bool> Handle(DeleteCarRequest request, CancellationToken cancellationToken)
        {
            Car carInDb = await _carRepo.GetByIdAsync(request.CarId);

            if (carInDb != null)
            {
                await _carRepo.DeleteAsync(carInDb);
                return true;
            }
            return false;
        }
    }
}
