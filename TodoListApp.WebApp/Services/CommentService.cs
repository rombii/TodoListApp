namespace TodoListApp.WebApp.Services;
using TodoListApp.WebApp.Models.Post;
using TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models.Put;


public class CommentService : ICommentService
{
    private readonly IServiceHelper helper;

    public CommentService(IServiceHelper helper)
    {
        this.helper = helper;
    }

    public async Task<HttpResponseMessage> AddCommentAsync(TaskCommentPostModel model)
    {
        return await this.helper.CallApiWithTokenAsync(async client => await client.PostAsJsonAsync("/api/taskcomment", model));
    }

    public async Task<HttpResponseMessage> EditCommentAsync(TaskCommentPutModel model)
    {
        return await this.helper.CallApiWithTokenAsync(async client => await client.PutAsJsonAsync($"/api/taskcomment/", model));
    }

    public async Task<HttpResponseMessage> DeleteCommentAsync(Guid commentId)
    {
        return await this.helper.CallApiWithTokenAsync(async client => await client.DeleteAsync($"/api/taskcomment/{commentId}"));
    }
}
