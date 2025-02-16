namespace TodoListApp.WebApi.Models.Post;

public class TaskTagPostModel
{
    public string Tag { get; set; } = null!;

    public Guid ListId { get; set; }
}
