namespace TodoListApp.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TodoListApp.WebApi.Models.Post;
using TodoListApp.WebApi.Models.Put;
using TodoListApp.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class TaskTagController : ControllerBase
{
    private readonly ITaskTagDatabaseService tagService;

    public TaskTagController(ITaskTagDatabaseService tagService)
    {
        this.tagService = tagService;
    }

    [HttpGet("task/{todoTaskId:guid}")]
    public async Task<IActionResult> GetTagsForTodoTaskAsync(Guid todoTaskId)
    {
        var tags = await this.tagService.GetAllTagsForTask(todoTaskId);
        return this.Ok(tags);
    }

    [HttpGet("{tagId:guid}")]
    public async Task<IActionResult> GetTasksForTagAsync(Guid tagId)
    {
        var tasks = await this.tagService.GetAllTasksForTag(tagId);
        return this.Ok(tasks);
    }


    [HttpGet("/list/{listId:guid}")]
    public async Task<IActionResult> GetTagsForListAsync(Guid listId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var tags = await this.tagService.GetAllTagsForList(listId, userLogin);
        return this.Ok(tags);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTagAsync([FromBody] TaskTagPostModel newTask)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.tagService.AddTag(newTask, userLogin);
        return this.NoContent();
    }

    [HttpPost("/assign/")]
    public async Task<IActionResult> AssignTask([FromQuery] Guid tagId, [FromQuery] Guid taskId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.tagService.AssignTag(tagId, taskId, userLogin);

        return this.NoContent();
    }

    [HttpDelete("{tagId:guid}")]
    public async Task<IActionResult> DeleteTaskAsync(Guid tagId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.tagService.DeleteTag(tagId, userLogin);
        return this.NoContent();
    }
}

