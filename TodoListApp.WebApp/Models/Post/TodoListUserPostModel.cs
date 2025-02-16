namespace TodoListApp.WebApp.Models.Post;
using System.ComponentModel.DataAnnotations;

public class TodoListUserPostModel
{
    [Required(ErrorMessage = "Login is required.")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Repeat Password is required.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string RepeatPassword { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }
}
