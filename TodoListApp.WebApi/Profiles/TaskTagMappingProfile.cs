namespace TodoListApp.WebApi.Profiles;
using AutoMapper;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;

public class TaskTagMappingProfile : Profile
{
    public TaskTagMappingProfile()
    {
        this.CreateMap<TaskTagEntity, TaskTagModel>();

        this.CreateMap<TaskTagModel, TaskTagEntity>()
            .ForMember(dest => dest.Tasks, opt => opt.Ignore());
        this.CreateMap<TaskTagPostModel, TaskTagEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(
                src => Guid.NewGuid()));
    }
}
