namespace TodoListApp.WebApp.Models;
using System.Text.Json.Serialization;

public class LoginModel
{
    [JsonPropertyName("login")]
    public string Login { get; set; } = null!;

    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;
}
