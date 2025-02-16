namespace TodoListApp.WebApi.Models;

public class TaskCommentModel
{
    public Guid Id { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
