namespace TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Models.Post;

public interface IAuthService
{
    Task<string> LoginAsync(LoginModel loginModel);

    Task<HttpResponseMessage> RegisterAsync(TodoListUserPostModel todoListUserPostModel);

    Task LogoutAsync();
}
