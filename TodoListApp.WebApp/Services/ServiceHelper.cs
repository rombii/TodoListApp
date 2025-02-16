namespace TodoListApp.WebApp.Services;
using System.Net.Http.Headers;
using TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models;
using System.Text.Json;

public class ServiceHelper : IServiceHelper
{
    private readonly HttpClient httpClient;
    private readonly IHttpContextAccessor httpContextAccessor;

    public ServiceHelper(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        this.httpClient = httpClient;
        this.httpContextAccessor = httpContextAccessor;
    }

    public Task<string?> GetAccessTokenAsync()
    {
        return Task.FromResult(this.httpContextAccessor.HttpContext?.Session.GetString("AccessToken"));
    }

    public async Task RefreshTokenAsync()
    {
        var accessToken = this.httpContextAccessor.HttpContext?.Session.GetString("AccessToken");
        var response = await this.httpClient.GetAsync($"api/user/token/{accessToken}");

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(result);
            this.httpContextAccessor.HttpContext?.Session.SetString("AccessToken", tokenResponse.AccessToken);
        }
    }

    public async Task<HttpResponseMessage> CallApiWithTokenAsync(Func<HttpClient, Task<HttpResponseMessage>> apiCall)
    {
        var token = await this.GetAccessTokenAsync();
        this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await apiCall(this.httpClient);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            await this.RefreshTokenAsync();
            token = await this.GetAccessTokenAsync();
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await apiCall(this.httpClient);
        }

        return response;
    }
}
