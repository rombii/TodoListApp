namespace TodoListApp.WebApi.Entities;
using System.ComponentModel.DataAnnotations;

public class TodoListUserEntity
{
    [Key]
    [Required]
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}
