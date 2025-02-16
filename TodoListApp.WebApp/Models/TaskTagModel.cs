namespace TodoListApp.WebApp.Models;
using System.Text.Json.Serialization;


public class TaskTagModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("tag")]
    public string Tag { get; set; } = null!;
}
