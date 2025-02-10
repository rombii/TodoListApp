namespace TodoListApp.WebApi.Models.Post;
using System.ComponentModel.DataAnnotations;


public class TaskCommentPostModel
{
    [Required]
    public string Comment { get; set; }

    [Required]
    public Guid TaskId { get; set; }
}
