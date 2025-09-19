using Application.IRepositories;
using Application.Models;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Images.Queries
{
    public class GetImageByIdRequest : IRequest<ImageDto>
    {
        public int ImageId { get; set; }
        public GetImageByIdRequest(int imageId)
        {
            ImageId = imageId;        
        }
    }

    public class GetImageByIdRequestHandler : IRequestHandler<GetImageByIdRequest, ImageDto>
    {
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;

        public GetImageByIdRequestHandler(IImageRepo imageRepo, IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<ImageDto> Handle(GetImageByIdRequest request, CancellationToken cancellationToken)
        {
            Image imageInDb = await _imageRepo.GetByIdAsync(request.ImageId);
            if(imageInDb != null)
            {
                ImageDto imageDto = _mapper.Map<ImageDto>(imageInDb);
                return imageDto;
            }
            return null;
        }
    }
}
