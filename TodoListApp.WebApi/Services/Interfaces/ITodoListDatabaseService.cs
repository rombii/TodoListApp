namespace TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models.Put;
using TodoListApp.WebApi.Models.Post;
using TodoListApp.WebApi.Models;

public interface ITodoListDatabaseService
{
    Task<List<TodoListModel>> GetTodoListsCreatedForUserAsync(string? issuer);

    Task CreateTodoListAsync(TodoListPostModel todoList, string? issuer);

    Task UpdateTodoListAsync(TodoListPutModel todoList, string? issuer);

    Task DeleteTodoListAsync(Guid listId, string? issuer);
}
