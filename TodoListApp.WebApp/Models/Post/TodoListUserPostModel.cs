namespace TodoListApp.WebApp.Models.Post;
using System.ComponentModel.DataAnnotations;

public class TodoListUserPostModel
{
    [Required(ErrorMessage = "Login is required.")]
    public string Login { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Repeat Password is required.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string RepeatPassword { get; set; } = null!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; } = null!;
}
