namespace TodoListApp.WebApi.Models.Post;

public class TodoListUserPostModel
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string RepeatPassword { get; set; } = null!;

    public string Email { get; set; } = null!;
}
