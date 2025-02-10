namespace TodoListApp.WebApi.Models.Post;

public class TaskTagPostModel
{
    public string Tag { get; set; }

    public Guid ListId { get; set; }
}
