namespace TodoListApp.WebApi.Profiles;
using AutoMapper;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        this.CreateMap<TodoTaskEntity, TodoTaskModel>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src =>
                    src.Tags.Select(tag => new TaskTagModel { Id = tag.Id, Tag = tag.Tag, })));

        this.CreateMap<TodoTaskEntity, TodoTaskWithCommentsModel>()
            .IncludeBase<TodoTaskEntity, TodoTaskModel>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src =>
                src.Comments.Select(
                comment => new TaskCommentModel
                {
                    Id = comment.Id,
                    Comment = comment.Comment,
                })));

        this.CreateMap<TodoTaskPostModel, TodoTaskEntity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => false));
    }
}
