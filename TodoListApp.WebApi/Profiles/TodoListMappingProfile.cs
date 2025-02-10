namespace TodoListApp.WebApi.Profiles;
using AutoMapper;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models;

public class TodoListMappingProfile : Profile
{
    public TodoListMappingProfile()
    {
        this.CreateMap<TodoListModel, TodoListEntity>();

        this.CreateMap<TodoListEntity, TodoListModel>();
    }
}
