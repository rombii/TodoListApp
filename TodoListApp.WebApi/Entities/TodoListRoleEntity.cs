namespace TodoListApp.WebApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class TodoListRoleEntity
{
    public Guid Id { get; set; }

    [ForeignKey("List")]
    public Guid ListId { get; set; }

    public TodoListEntity List { get; set; } = null!;

    public string User { get; set; } = null!;

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }

    public RoleEntity Role { get; set; } = null!;
}
