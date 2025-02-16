namespace TodoListApp.WebApi.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("TodoTask")]
public class TodoTaskEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime DueDate { get; set; }

    [ForeignKey("List")]
    public Guid ListId { get; set; }

    public TodoListEntity List { get; set; } = null!;

    public List<TaskCommentEntity> Comments { get; set; } = null!;

    public List<TaskTagEntity> Tags { get; set; } = null!;
}
