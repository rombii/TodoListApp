namespace TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Models.Put;

public interface ITodoTaskService
{
    Task<TodoTaskModel[]> GetTasksForTodoListAsync(Guid todoListId);

    Task<TodoTaskModel[]> GetTasksForUserAsync();

    Task<TodoTaskDetailsModel> GetTaskAsync(Guid taskId);

    Task CreateTaskAsync(TodoTaskPostModel task);

    Task DeleteTaskAsync(Guid taskId);

    Task UpdateTaskAsync(TodoTaskPutModel task);

    Task ChangeTaskStatusAsync(Guid taskId);

    Task<TodoTaskModel[]> GetOverdueTasksAsync();
}
