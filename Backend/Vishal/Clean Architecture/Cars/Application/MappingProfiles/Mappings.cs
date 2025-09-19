using Application.Models;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    //To act as a automapper - a mapping profile configurator we have inherit Profile
    public class Mappings :Profile
    {
        public Mappings()
        {
            CreateMap<NewCar, Car>();
            CreateMap<Car, CarDto>();        //we have to only map internal model to outgoing model

            CreateMap<NewImage, Image>();
            CreateMap<Image, ImageDto>();
        }
    }
}
