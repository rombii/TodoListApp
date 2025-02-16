namespace TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;
using TodoListApp.WebApi.Models.Put;

public interface ITodoTaskDatabaseService
{
    Task<List<TodoTaskModel>> GetTasksForTodoListAsync(Guid listId, string? issuer);

    Task<List<TodoTaskModel>> GetTasksForUserAsync(string? issuer);

    Task<TodoTaskWithCommentsModel> GetTask(Guid id, string? issuer);

    Task CreateTodoTaskAsync(TodoTaskPostModel model, string? issuer);

    Task DeleteTodoTaskAsync(Guid id, string? issuer);

    Task UpdateTodoTaskAsync(TodoTaskPutModel model, string? issuer);

    Task UpdateTaskStatusAsync(Guid taskId, string? issuer);

    Task<List<TodoTaskModel>> GetOverdueTasksForUserAsync(string? issuer);
}
