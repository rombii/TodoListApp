namespace TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Models.Post;
using TodoListApp.WebApp.Models.Put;

public interface ITodoListService
{
    Task<TodoListModel[]> GetTodoListsAsync();

    Task CreateTodoListAsync(TodoListPostModel todoList);

    Task UpdateTodoListAsync(TodoListPutModel todoList);

    Task DeleteTodoListAsync(Guid listId);
}
