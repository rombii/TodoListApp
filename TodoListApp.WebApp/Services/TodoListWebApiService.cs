
namespace TodoListApp.WebApp.Services;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Services.Interfaces;
using System.Text;
using Newtonsoft.Json;

public class TodoListWebApiService : ITodoListWebApiService
{
    private readonly HttpClient httpClient;

    // ig move this to appsettings.json
    private const string ApiURL = "https://localhost:7177/api/TodoList";

    public TodoListWebApiService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<TodoListWebApiModel>> GetTodoListsCreatedByUserAsync(Guid userId)
    {
        var response = await this.httpClient.GetAsync(ApiURL + "/" + userId);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<TodoListWebApiModel>>(result);
    }

    public async Task CreateTodoListAsync(TodoListWebApiModel todoList)
    {
        var content = new StringContent(JsonConvert.SerializeObject(todoList), Encoding.UTF8, "application/json");
        var response = await this.httpClient.PostAsync(ApiURL, content);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTodoListAsync(Guid listId)
    {
        var response = await this.httpClient.DeleteAsync($"{ApiURL}/{listId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateTodoListAsync(TodoListWebApiModel todoList)
    {
        var content = new StringContent(JsonConvert.SerializeObject(todoList), Encoding.UTF8, "application/json");
        var response = await this.httpClient.PutAsync($"{ApiURL}/{todoList.Id}", content);
        response.EnsureSuccessStatusCode();
    }
}
