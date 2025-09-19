using Application.IRepository;
using Application.Models;
using Application.PipelineBehaviours.Contracts;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Cars.Queries
{
    public class GetCarByIdRequest :IRequest<CarDto>, ICacheable
    {
        public int CarId { get; set; }

        public string CacheKey { get; set; }
        public bool ByPassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetCarByIdRequest(int carId)
        {
            CarId = carId;
            CacheKey = $"GetCarById:{CarId}";
        }
    }

    public class GetCarByIdRequestHandler : IRequestHandler<GetCarByIdRequest, CarDto>
    {
        //to get data from Db
        private readonly ICarRepo _carRepo;
        private readonly IMapper _mapper;

        public GetCarByIdRequestHandler(ICarRepo carRepo, IMapper mapper)
        {
            _mapper = mapper;
            _carRepo = carRepo;
        }
        public async Task<CarDto> Handle(GetCarByIdRequest request, CancellationToken cancellationToken)
        {
            Car carInDb = await _carRepo.GetByIdAsync(request.CarId);

            if(carInDb != null)
            {
                //Mapping from car => carDto
                CarDto carDto = _mapper.Map<CarDto>(carInDb);
                return carDto;
            }
            return null;
        }
    }
}
