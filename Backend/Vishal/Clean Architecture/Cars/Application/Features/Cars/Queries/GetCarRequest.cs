using Application.IRepository;
using Application.Models;
using Application.PipelineBehaviours.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Cars.Queries
{
    public class GetCarRequest : IRequest<List<CarDto>>, ICacheable
    {
        //we dont have any parameter to pass for GetAllAsync() in CarRepo so don't need to inject anything in ctor
        public string CacheKey { get; set; }
        public bool ByPassCache { get ; set ; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetCarRequest()
        {
            CacheKey = "getCars";
        }
    }

    public class GetCarRequestHandler : IRequestHandler<GetCarRequest, List<CarDto>>
    {
        private readonly ICarRepo _carRepo;
        private readonly IMapper _mapper;

        public GetCarRequestHandler(ICarRepo carRepo, IMapper mapper)
        {
            _carRepo = carRepo;
            _mapper = mapper;
        }

        public async Task<List<CarDto>>Handle(GetCarRequest request,CancellationToken cancellationToken)
        {
            List<Car> cars = await _carRepo.GetAllAsync();

            if(cars != null)
            {
                //Mapping from car => carDto
                List<CarDto> carDtos = _mapper.Map<List<CarDto>>(cars);
                return carDtos;
            }
            return null;
        }
    }

}
