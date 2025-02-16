namespace TodoListApp.WebApp.Models;
using System.Text.Json.Serialization;

public class LoginModel
{
    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}
