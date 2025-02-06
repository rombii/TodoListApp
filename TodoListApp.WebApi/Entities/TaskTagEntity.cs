namespace TodoListApp.WebApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("TaskTag")]
public class TaskTagEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Tag { get; set; }

    public List<TodoTaskEntity> Tasks { get; set; }
}
