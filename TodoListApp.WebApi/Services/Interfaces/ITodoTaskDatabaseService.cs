namespace TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;
using TodoListApp.WebApi.Models.Put;

public interface ITodoTaskDatabaseService
{
    Task<List<TodoTaskModel>> GetTasksForTodoListAsync(Guid listId);

    Task<TodoTaskWithCommentsModel?> GetTask(Guid id);

    Task CreateTodoTaskAsync(TodoTaskPostModel model);

    Task DeleteTodoTaskAsync(Guid id);

    Task UpdateTodoTaskAsync(TodoTaskPutModel model);
}
