namespace TodoListApp.WebApi.Models;

public class TodoTaskModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime DueDate { get; set; }

    public Guid ListId { get; set; }

    public List<TaskTagModel> Tags { get; set; }

}
