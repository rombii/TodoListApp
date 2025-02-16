namespace TodoListApp.WebApi.Entities;
using System.ComponentModel.DataAnnotations;

public class RoleEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Role { get; set; } = null!;
}
