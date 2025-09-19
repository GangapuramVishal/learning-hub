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

namespace Application.Features.Images.Commands
{
    public class UpdateImageRequest :IRequest <bool>
    {
        public UpdateImage UpdateImage { get; set;}

        public UpdateImageRequest(UpdateImage updateImage)
        {
            UpdateImage = updateImage;
        }
    }

    public class UpdateImageRequestHandler : IRequestHandler<UpdateImageRequest, bool>
    {
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;

        public UpdateImageRequestHandler(IImageRepo imageRepo, IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateImageRequest request, CancellationToken cancellationToken)
        {
            Image imageInDb = await _imageRepo.GetByIdAsync(request.UpdateImage.Id);
            if (imageInDb != null)
            {
                //Update
                imageInDb.Name = request.UpdateImage.Name;
                imageInDb.Path = request.UpdateImage.Path;

                await _imageRepo.UpdateAsync(imageInDb);
                return true;
            }
            return false;
        }
    }
}
