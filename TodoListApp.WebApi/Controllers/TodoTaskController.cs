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

    [HttpGet("list/{todoListId:guid}")]
    public async Task<IActionResult> GetTasksForTodoListAsync(Guid todoListId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var tasks = await this.taskService.GetTasksForTodoListAsync(todoListId, userLogin);
        return this.Ok(tasks);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var task = await this.taskService.GetTask(taskId, userLogin);
        return this.Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync([FromBody] TodoTaskPostModel newTask)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.taskService.CreateTodoTaskAsync(newTask, userLogin);
        return this.NoContent();
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteTaskAsync(Guid taskId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.taskService.DeleteTodoTaskAsync(taskId, userLogin);
        return this.NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTaskAsync([FromBody] TodoTaskPutModel updatedTask)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.taskService.UpdateTodoTaskAsync(updatedTask, userLogin);
        return this.NoContent();
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdueTasksAsync()
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var tasks = await this.taskService.GetOverdueTasksForUserAsync(userLogin);
        return this.Ok(tasks);
    }
}
