namespace TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Models.Post;


public interface ITagService
{
    Task<TaskTagModel[]> GetTagsForListAsync(Guid listId);

    Task AddTagAsync(TaskTagPostModel model);

    Task RemoveTagAsync(Guid tagId);

    Task<HttpResponseMessage> AssignTagToTaskAsync(Guid taskId, Guid tagId);

    Task<HttpResponseMessage> RemoveTagFromTaskAsync(Guid taskId, Guid tagId);
}
