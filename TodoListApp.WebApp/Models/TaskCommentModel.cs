namespace TodoListApp.WebApp.Models;
using System.Text.Json.Serialization;

public class TaskCommentModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("comment")]
    public string Comment { get; set; }

    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
}
