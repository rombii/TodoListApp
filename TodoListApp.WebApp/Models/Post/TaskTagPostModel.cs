namespace TodoListApp.WebApp.Models.Post;
using System.Text.Json.Serialization;

public class TaskTagPostModel
{
    [JsonPropertyName("tag")]
    public string Tag { get; set; } = null!;

    [JsonPropertyName("listId")]
    public Guid ListId { get; set; }
}
