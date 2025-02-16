namespace TodoListApp.WebApp.Services;
using System.Text;
using System.Text.Json;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Models.Put;
using TodoListApp.WebApp.Services.Interfaces;

public class TodoTaskService : ITodoTaskService
{
    private readonly IServiceHelper helper;

    public TodoTaskService(IServiceHelper helper)
    {
        this.helper = helper;
    }

    public async Task<TodoTaskModel[]> GetTasksForTodoListAsync(Guid todoListId)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.GetAsync($"api/task/list/{todoListId}"));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TodoTaskModel[]>(result)!;
    }

    public async Task<TodoTaskModel[]> GetTasksForUserAsync()
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.GetAsync("api/task/user"));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TodoTaskModel[]>(result)!;
    }

    public async Task<TodoTaskDetailsModel> GetTaskAsync(Guid taskId)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.GetAsync($"api/task/{taskId}"));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TodoTaskDetailsModel>(result)!;
    }

    public async Task CreateTaskAsync(TodoTaskPostModel task)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.PostAsync("api/task", new StringContent(JsonSerializer.Serialize(task), Encoding.UTF8, "application/json")));
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteTaskAsync(Guid taskId)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.DeleteAsync($"api/task/{taskId}"));
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateTaskAsync(TodoTaskPutModel task)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.PutAsync("api/task", new StringContent(JsonSerializer.Serialize(task), Encoding.UTF8, "application/json")));
        response.EnsureSuccessStatusCode();
    }

    public async Task ChangeTaskStatusAsync(Guid taskId)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.PutAsync($"api/task/change_status/{taskId}", null));
        response.EnsureSuccessStatusCode();
    }

    public async Task<TodoTaskModel[]> GetOverdueTasksAsync()
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.GetAsync("api/task/overdue"));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TodoTaskModel[]>(result)!;
    }
}
