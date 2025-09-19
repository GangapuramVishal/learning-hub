using AutoMapper;
using BookInventory.BLL.DTO;
using BookInventorty.DAL.Models;

namespace BookInventory.API
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        {
            CreateMap<Book, CreateDTO>();
            CreateMap<CreateDTO, Book>();
            CreateMap<Book, UpdateDTO>().ReverseMap();
        }
    }
}
