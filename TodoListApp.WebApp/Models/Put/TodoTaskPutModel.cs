namespace TodoListApp.WebApp.Models.Put;
using System.Text.Json.Serialization;


public class TodoTaskPutModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; set; }

    [JsonPropertyName("dueDate")]
    public DateTime DueDate { get; set; }

    [JsonIgnore]
    public Guid ListId { get; set; }
}
