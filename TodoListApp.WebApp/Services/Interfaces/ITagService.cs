namespace TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models;

public interface ITagService
{
    Task<TaskTagModel[]> GetTagsForListAsync(Guid listId);

    Task<HttpResponseMessage> AssignTagToTaskAsync(Guid taskId, Guid tagId);

    Task<HttpResponseMessage> RemoveTagFromTaskAsync(Guid taskId, Guid tagId);
}
