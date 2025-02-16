namespace TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models.Post;
using TodoListApp.WebApp.Models.Put;

public interface ICommentService
{
    Task<HttpResponseMessage> AddCommentAsync(TaskCommentPostModel model);

    Task<HttpResponseMessage> EditCommentAsync(TaskCommentPutModel model);

    Task<HttpResponseMessage> DeleteCommentAsync(Guid commentId);
}
