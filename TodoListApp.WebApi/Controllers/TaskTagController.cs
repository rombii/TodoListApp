namespace TodoListApp.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Models.Post;
using Services.Interfaces;
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

    /// <summary>
    /// Gets all tags for a specific task.
    /// </summary>
    /// <param name="todoTaskId">The ID of the task.</param>
    /// <returns>A list of tags.</returns>
    [HttpGet("task/{todoTaskId:guid}")]
    public async Task<IActionResult> GetTagsForTodoTaskAsync(Guid todoTaskId)
    {
        var tags = await this.tagService.GetAllTagsForTask(todoTaskId);
        return this.Ok(tags);
    }

    /// <summary>
    /// Gets all tasks associated with a specific tag.
    /// </summary>
    /// <param name="tagId">The ID of the tag.</param>
    /// <returns>A list of tasks.</returns>
    [HttpGet("{tagId:guid}")]
    public async Task<IActionResult> GetTasksForTagAsync(Guid tagId)
    {
        var tasks = await this.tagService.GetAllTasksForTag(tagId);
        return this.Ok(tasks);
    }

    /// <summary>
    /// Gets all tags for a specific list.
    /// </summary>
    /// <param name="listId">The ID of the list.</param>
    /// <returns>A list of tags.</returns>
    [HttpGet("list/{listId:guid}")]
    public async Task<IActionResult> GetTagsForListAsync(Guid listId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var tags = await this.tagService.GetAllTagsForList(listId, userLogin);
        return this.Ok(tags);
    }

    /// <summary>
    /// Creates a new tag.
    /// </summary>
    /// <param name="newTask">The new tag model.</param>
    /// <returns>No content.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateTagAsync([FromBody] TaskTagPostModel newTask)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.tagService.AddTag(newTask, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Assigns a tag to a task.
    /// </summary>
    /// <param name="tagId">The ID of the tag.</param>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>No content.</returns>
    [HttpPost("assign/")]
    public async Task<IActionResult> AssignTag([FromQuery] Guid tagId, [FromQuery] Guid taskId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.tagService.AssignTag(tagId, taskId, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Removes a tag from a task.
    /// </summary>
    /// <param name="tagId">The ID of the tag.</param>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>No content.</returns>
    [HttpDelete("remove/")]
    public async Task<IActionResult> RemoveTag([FromQuery] Guid tagId, [FromQuery] Guid taskId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.tagService.RemoveTag(tagId, taskId, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Deletes a tag by its ID.
    /// </summary>
    /// <param name="tagId">The ID of the tag to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{tagId:guid}")]
    public async Task<IActionResult> DeleteTagAsync(Guid tagId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.tagService.DeleteTag(tagId, userLogin);
        return this.NoContent();
    }
}
