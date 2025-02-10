namespace TodoListApp.WebApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;

public class TodoListRoleEntity
{
    public Guid Id { get; set; }

    [ForeignKey("List")]
    public Guid ListId { get; set; }

    public TodoListEntity List { get; set; }

    public string User { get; set; }

    [ForeignKey("Role")]
    public Guid RoleId { get; set; }

    public RoleEntity Role { get; set; }
}
