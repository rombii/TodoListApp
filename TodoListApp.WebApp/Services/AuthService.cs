namespace TodoListApp.WebApp.Services;
using System.Text;
using TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models.Post;
using System.Text.Json;
using System.Text.Json.Serialization;
using TodoListApp.WebApp.Models;

public class AuthService : IAuthService
{
    private readonly HttpClient httpClient;
    private readonly IHttpContextAccessor contextAccessor;

    public AuthService(HttpClient httpClient, IHttpContextAccessor contextAccessor)
    {
        this.httpClient = httpClient;
        this.contextAccessor = contextAccessor;
    }

    public async Task<string> LoginAsync(LoginModel loginModel)
    {
        var content = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");
        var response = await this.httpClient.PostAsync("api/user/login", content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(result);
            if (tokenResponse != null)
            {
                return tokenResponse.AccessToken;
            }
        }

        return null!;
    }

    public async Task<HttpResponseMessage> RegisterAsync(TodoListUserPostModel todoListUserPostModel)
    {
        var content = new StringContent(JsonSerializer.Serialize(todoListUserPostModel), Encoding.UTF8, "application/json");
        var response = await this.httpClient.PostAsync("api/user/register", content);

        return response;
    }

    public async Task LogoutAsync()
    {
        var response = await this.httpClient.PostAsync("api/user/logout", null);
        if (response.IsSuccessStatusCode)
        {
            this.contextAccessor.HttpContext?.Session.Remove("AccessToken");
        }
    }
}
