namespace TodoListApp.WebApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("TaskTag")]
public class TaskTagEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Tag { get; set; } = null!;

    [ForeignKey("List")]
    public Guid ListId { get; set; }

    public TodoListEntity List { get; set; } = null!;

    public List<TodoTaskEntity> Tasks { get; set; } = null!;
}
