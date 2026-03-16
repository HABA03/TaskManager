using AutoMapper;
using TM.Application.DTO.TaskItem.Create;
using TM.Application.DTO.TaskItem.GetById;
using TM.Application.DTO.TaskItem.Update;
using TM.Domain.Entity;

namespace TM.API.AutoMapperProfile.MapperProfile;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<TaskItem, CreateTaskRequest>().ReverseMap();
        CreateMap<TaskItem, UpdateTaskRequest>().ReverseMap();
        CreateMap<TaskItem, GetTaskByIdResponse>().ReverseMap();
        CreateMap<TaskItem, GetAllTaskResponse>().ReverseMap();
    }
}