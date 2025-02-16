namespace TodoListApp.WebApp.Services.Interfaces;

public interface IServiceHelper
{
    Task<string?> GetAccessTokenAsync();

    Task RefreshTokenAsync();

    Task<HttpResponseMessage> CallApiWithTokenAsync(Func<HttpClient, Task<HttpResponseMessage>> apiCall);
}
