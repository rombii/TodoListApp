namespace TodoListApp.WebApi.Models.Post;

public class TodoListPostModel
{
    public string Title { get; set; }

    public string Description { get; set; }

    public Guid CreatedBy { get; set; }
}
