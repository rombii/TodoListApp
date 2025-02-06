namespace TodoListApp.WebApi.Models;

public class TodoListModel
{
    public Guid? Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }
}
