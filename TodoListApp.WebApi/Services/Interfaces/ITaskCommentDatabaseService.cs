namespace TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;

public interface ITaskCommentDatabaseService
{
    Task<List<TaskCommentModel>> GetCommentsForTaskAsync(Guid taskId, string? issuer);

    Task AddCommentAsync(TaskCommentPostModel model, string? issuer);

    Task RemoveComment(Guid commentId, string? issuer);
}
