namespace TodoListApp.WebApp.Models.Post;
using System.Text.Json.Serialization;


public class TodoListPostModel
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

