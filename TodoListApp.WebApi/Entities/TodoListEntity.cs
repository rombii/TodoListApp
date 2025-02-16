namespace TodoListApp.WebApi.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TodoList")]
public class TodoListEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    [MaxLength(500)]
    public string Description { get; set; } = null!;

    [Required]
    public DateTime CreatedDate { get; set; }

    public List<TodoListRoleEntity> ListRoles { get; set; } = null!;

    public List<TaskTagEntity> Tags { get; set; } = null!;
}
