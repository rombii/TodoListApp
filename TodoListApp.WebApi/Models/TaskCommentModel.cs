namespace TodoListApp.WebApi.Models;

public class TaskCommentModel
{
    public Guid Id { get; set; }

    public string Comment { get; set; }

    public DateTime CreatedDate { get; set; }
}
