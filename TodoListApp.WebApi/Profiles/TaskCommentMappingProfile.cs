namespace TodoListApp.WebApi.Profiles;
using AutoMapper;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;

public class TaskCommentMappingProfile : Profile
{
    public TaskCommentMappingProfile()
    {
        this.CreateMap<TaskCommentEntity, TaskCommentModel>();
        this.CreateMap<TaskCommentPostModel, TaskCommentEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(
                src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(
                src => DateTime.UtcNow));
    }
}
