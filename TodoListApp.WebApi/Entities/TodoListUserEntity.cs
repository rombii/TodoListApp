namespace TodoListApp.WebApi.Entities;
using System.ComponentModel.DataAnnotations;

public class TodoListUserEntity
{
    [Key]
    [Required]
    public string Login { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string RefreshToken { get; set; }
}
