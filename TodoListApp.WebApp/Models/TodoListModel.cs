namespace TodoListApp.WebApp.Models;
using System.Text.Json.Serialization;

public class TodoListModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]

    public string Description { get; set; }

    [JsonPropertyName("createdDate")]

    public DateTime CreatedDate { get; set; }
}
