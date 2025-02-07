namespace TodoListApp.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models.Post;
using TodoListApp.WebApi.Models.Put;

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
        var tasks = await this.taskService.GetTasksForTodoListAsync(todoListId);
        return this.Ok(tasks);
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetTask(Guid taskId)
    {
        var task = await this.taskService.GetTask(taskId);

        if (task == null)
        {
            return this.NotFound();
        }

        return this.Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskAsync([FromBody] TodoTaskPostModel newTask)
    {
        if (newTask.ListId == Guid.Empty)
        {
            return this.BadRequest("TodoListId is required.");
        }

        await this.taskService.CreateTodoTaskAsync(newTask);
        return this.Ok();
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteTaskAsync(Guid taskId)
    {
        await this.taskService.DeleteTodoTaskAsync(taskId);
        return this.NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTaskAsync([FromBody] TodoTaskPutModel updatedTask)
    {
        await this.taskService.UpdateTodoTaskAsync(updatedTask);
        return Ok("Task updated successfully.");
    }

    // TODO
    // This prolly will use token so we look into it later wwparaStare
 //   [HttpGet("overdue")]
 //   public async Task<IActionResult> GetOverdueTasksAsync()
 //   {
 //       var tasks = await taskService.GetOverdueTodoTasksAsync();
 //       return Ok(tasks);
 //   }
}
