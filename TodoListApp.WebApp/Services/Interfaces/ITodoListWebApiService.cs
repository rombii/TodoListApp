
namespace TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models;

public interface ITodoListWebApiService
{
    Task<List<TodoListWebApiModel>> GetTodoListsCreatedByUserAsync(Guid userId);

    Task CreateTodoListAsync(TodoListWebApiModel todoList);

    Task UpdateTodoListAsync(TodoListWebApiModel todoList);

    Task DeleteTodoListAsync(Guid listId);
}
