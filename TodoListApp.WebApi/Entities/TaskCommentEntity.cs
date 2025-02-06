namespace TodoListApp.WebApi.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

[Table("TaskComment")]
public class TaskCommentEntity
{
    [Key]
    public Guid Id { get; set; }

    public string Comment { get; set; }

    public Guid TaskId { get; set; }
}
