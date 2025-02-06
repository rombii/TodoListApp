namespace TodoListApp.WebApi.Models.Put;

public class TodoListPutModel
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
}
