namespace TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models.Put;
using TodoListApp.WebApi.Models.Post;

public interface ITaskCommentDatabaseService
{
    Task AddCommentAsync(TaskCommentPostModel model, string? issuer);

    Task RemoveComment(Guid commentId, string? issuer);

    Task UpdateComment(TaskCommentPutModel model, string? issuer);
}
