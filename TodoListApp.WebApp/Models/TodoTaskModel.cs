namespace TodoListApp.WebApp.Models;

using System.Text.Json.Serialization;

public class TodoTaskModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }

    [JsonPropertyName("dueDate")]
    public DateTime DueDate { get; set; }

    [JsonPropertyName("listId")]
    public Guid ListId { get; set; }

    [JsonPropertyName("tags")]
    public List<TaskTagModel> Tags { get; set; } = null!;
}
