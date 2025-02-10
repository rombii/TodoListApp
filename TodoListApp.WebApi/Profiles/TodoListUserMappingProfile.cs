namespace TodoListApp.WebApi.Profiles;
using AutoMapper;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Generators;
using TodoListApp.WebApi.Models.Post;

public class TodoListUserMappingProfile : Profile
{
    public TodoListUserMappingProfile()
    {
        this.CreateMap<TodoListUserPostModel, TodoListUserEntity>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(
                src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
            .ForMember(dest => dest.RefreshToken, opt => opt.Ignore());
    }
}
