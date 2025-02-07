namespace TodoListApp.WebApi.Models;

public class TodoTaskWithCommentsModel : TodoTaskModel
{
    public List<TaskCommentModel> Comments { get; set; }
}
