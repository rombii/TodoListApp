namespace TodoListApp.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models.Post;
using TodoListApp.WebApi.Models.Put;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITodoTaskDatabaseService taskService;

    public TaskController(ITodoTaskDatabaseService taskService)
    {
        this.taskService = taskService;
    }

    /// <summary>
    /// Gets all tasks for a specific todolist.
    /// </summary>
    /// <param name="todoListId">The ID of the todolist.</param>
    /// <returns>A list of tasks.</returns>
    [HttpGet("list/{todoListId:guid}")]
    public async Task<IActionResult> GetTasksForTodoListAsync(Guid todoListId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var tasks = await this.taskService.GetTasksForTodoListAsync(todoListId, userLogin);
        return this.Ok(tasks);
    }

    /// <summary>
    /// Gets all tasks for the authenticated user.
    /// </summary>
    /// <returns>A list of tasks.</returns>
    [HttpGet("user")]
    public async Task<IActionResult> GetTasksForUserAsync()
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var tasks = await this.taskService.GetTasksForUserAsync(userLogin);
        return this.Ok(tasks);
    }

    /// <summary>
    /// Gets a specific task by its ID.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>The task.</returns>
    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var task = await this.taskService.GetTask(taskId, userLogin);
        return this.Ok(task);
    }

    /// <summary>
    /// Creates a new task.
    /// </summary>
    /// <param name="newTask">The new task model.</param>
    /// <returns>No content.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync([FromBody] TodoTaskPostModel newTask)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.taskService.CreateTodoTaskAsync(newTask, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Deletes a task by its ID.
    /// </summary>
    /// <param name="taskId">The ID of the task to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteTaskAsync(Guid taskId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.taskService.DeleteTodoTaskAsync(taskId, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Updates an existing task.
    /// </summary>
    /// <param name="updatedTask">The updated task model.</param>
    /// <returns>No content.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTaskAsync([FromBody] TodoTaskPutModel updatedTask)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.taskService.UpdateTodoTaskAsync(updatedTask, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Changes the status of a task.
    /// </summary>
    /// <param name="taskId">The ID of the task.</param>
    /// <returns>No content.</returns>
    [HttpPut("change_status/{taskId:guid}")]
    public async Task<IActionResult> ChangeTagStatus(Guid taskId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.taskService.UpdateTaskStatusAsync(taskId, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Gets all overdue tasks for the authenticated user.
    /// </summary>
    /// <returns>A list of overdue tasks.</returns>
    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdueTasksAsync()
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var tasks = await this.taskService.GetOverdueTasksForUserAsync(userLogin);
        return this.Ok(tasks);
    }
}
