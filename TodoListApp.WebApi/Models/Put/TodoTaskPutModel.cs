namespace TodoListApp.WebApi.Models.Put;

public class TodoTaskPutModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public DateTime DueDate { get; set; }
}
