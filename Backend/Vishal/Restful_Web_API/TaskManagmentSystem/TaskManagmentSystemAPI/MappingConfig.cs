using AutoMapper;
using TaskManagmentSystemAPI.Models;
using TaskManagmentSystemAPI.Models.Dto;

namespace TaskManagmentSystemAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<TaskModel, TaskDTO>();
            CreateMap<TaskDTO, TaskModel>();

            CreateMap<TaskModel, TaskCreateDTO>().ReverseMap();
            CreateMap<TaskModel, TaskUpdateDTO>().ReverseMap();
        }
    }
}
