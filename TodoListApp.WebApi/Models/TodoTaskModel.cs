namespace TodoListApp.WebApi.Models;

public class TodoTaskModel
{
    public Guid? Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime DueDate { get; set; }

    public Guid ListId { get; set; }

    public Guid CreatedBy { get; set; }
}
