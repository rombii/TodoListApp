namespace TodoListApp.WebApp.Models;
using System.Text.Json.Serialization;

public class TodoTaskDetailsModel : TodoTaskModel
{
    [JsonPropertyName("comments")]
    public List<TaskCommentModel> Comments { get; set; }
}
