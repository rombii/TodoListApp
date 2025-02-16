namespace TodoListApp.WebApp.Services;
using System.Text;
using System.Text.Json;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Models.Post;
using TodoListApp.WebApp.Models.Put;
using TodoListApp.WebApp.Services.Interfaces;

public class TodoListService : ITodoListService
{
    private readonly IServiceHelper helper;

    public TodoListService(IServiceHelper helper)
    {
        this.helper = helper;
    }


    public async Task<TodoListModel[]> GetTodoListsAsync()
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.GetAsync("api/todolist"));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TodoListModel[]>(result) !;
    }

    public async Task CreateTodoListAsync(TodoListPostModel todoList)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.PostAsync(
            "api/todolist",
            new StringContent(JsonSerializer.Serialize(todoList), Encoding.UTF8, "application/json")));
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateTodoListAsync(TodoListPutModel todoList)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.PutAsync(
            "api/todolist",
            new StringContent(JsonSerializer.Serialize(todoList), Encoding.UTF8, "application/json")));
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTodoListAsync(Guid listId)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.DeleteAsync($"api/todolist/{listId}"));
        response.EnsureSuccessStatusCode();
    }
}
