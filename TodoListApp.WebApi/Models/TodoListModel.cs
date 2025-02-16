namespace TodoListApp.WebApi.Models;

public class TodoListModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
