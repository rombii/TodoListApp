namespace TodoListApp.WebApp.Models.Post;

public class TaskCommentPostModel
{
    public Guid TaskId { get; set; }

    public string Comment { get; set; } = null!;
}
