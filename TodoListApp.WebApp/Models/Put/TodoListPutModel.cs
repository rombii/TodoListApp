namespace TodoListApp.WebApp.Models.Put;
using System.Text.Json.Serialization;

public class TodoListPutModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;
}
