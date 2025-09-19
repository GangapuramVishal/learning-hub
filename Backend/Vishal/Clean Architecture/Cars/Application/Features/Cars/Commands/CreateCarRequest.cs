using Application.IRepository;
using Application.Models;
using Application.PipelineBehaviours.Contracts;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Cars.Commands
{
    //indicate that,class represents a request that can be processed by a mediator.
    public class CreateCarRequest :IRequest<bool> , IValidatable                                    //Request class to receive request 
    {
        public NewCar CarRequest { get; set; }
        public CreateCarRequest(NewCar newCarRequest)
        {
            CarRequest = newCarRequest;
        }
    }

    public class CreateCarRequestHandler : IRequestHandler<CreateCarRequest, bool>           //RequestHandler class
    {
        private readonly ICarRepo _carRepo;
        private readonly IMapper _mapper;

        public CreateCarRequestHandler(ICarRepo carRepo, IMapper mapper)
        {
            _carRepo = carRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateCarRequest request, CancellationToken cancellationToken)
        {
            Car car = _mapper.Map<Car>(request.CarRequest);      //mapping Car - NewCarRequest

            car.manufactureDate = DateTime.Now;
            await _carRepo.AddNewAsync(car);

            return true;
        }
    }
}
