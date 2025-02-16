namespace TodoListApp.WebApp.Models;
using System.Text.Json.Serialization;

public class TokenResponse
{
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; set; } = null!;
}
