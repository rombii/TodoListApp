namespace TodoListApp.WebApp.Services;
using System.Text.Json;
using TodoListApp.WebApp.Models;
using System.Text;
using TodoListApp.WebApp.Models.Post;
using TodoListApp.WebApp.Services.Interfaces;

public class TagService : ITagService
{
    private readonly IServiceHelper helper;

    public TagService(IServiceHelper helper)
    {
        this.helper = helper;
    }

    public async Task<TaskTagModel[]> GetTagsForListAsync(Guid listId)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client =>
            await client.GetAsync($"api/tasktag/list/{listId}"));
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TaskTagModel[]>(result) !;
        }

        return Array.Empty<TaskTagModel>();
    }

    public async Task AddTagAsync(TaskTagPostModel model)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.PostAsync(
           "api/tasssktag",
           new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")));
        response.EnsureSuccessStatusCode();
    }

    public async Task RemoveTagAsync(Guid tagId)
    {
        var response = await this.helper.CallApiWithTokenAsync(async client => await client.DeleteAsync($"api/TaskTag/{tagId}"));
        response.EnsureSuccessStatusCode();
    }

    public async Task<HttpResponseMessage> AssignTagToTaskAsync(Guid taskId, Guid tagId)
    {
        return await this.helper.CallApiWithTokenAsync(async client =>
            await client.PostAsync($"api/tasktag/assign?tagId={tagId}&taskId={taskId}", null));
    }

    public async Task<HttpResponseMessage> RemoveTagFromTaskAsync(Guid taskId, Guid tagId)
    {
        return await this.helper.CallApiWithTokenAsync(async client =>
            await client.DeleteAsync($"api/tasktag/remove/?tagId={tagId}&taskId={taskId}"));
    }
}
