namespace TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;


public interface ITodoListUserDatabaseService
{
    Task<TodoListUserModel> Login(TodoListUserLoginModel model);

    Task Register(TodoListUserPostModel model);

    Task<TodoListUserModel> RefreshToken(string accessToken);

    Task Logout(string? issuer);
}
