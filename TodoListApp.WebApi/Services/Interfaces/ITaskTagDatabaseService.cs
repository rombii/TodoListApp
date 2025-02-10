namespace TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;


public interface ITaskTagDatabaseService
{
    Task<List<TaskTagModel>> GetAllTagsForList(Guid listId, string? issuer);

    Task<List<TodoTaskModel>> GetAllTasksForTag(Guid tagId);

    Task<List<TaskTagModel>> GetAllTagsForTask(Guid taskId);

    Task AddTag(TaskTagPostModel model, string? issuer);

    Task AssignTag(Guid tagId, Guid taskId, string? issuer);

    Task DeleteTag(Guid tagId, string? issuer);
}
