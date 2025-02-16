namespace TodoListApp.WebApp.Models;
using System.Text.Json.Serialization;

public class TodoListModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("description")]

    public string Description { get; set; } = null!;

    [JsonPropertyName("createdDate")]

    public DateTime CreatedDate { get; set; }
}
