namespace TodoListApp.WebApi.Controllers;
using TodoListApp.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Put;
using TodoListApp.WebApi.Models.Post;

[ApiController]
[Route("/api/[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ITodoListDatabaseService dbService;

    public TodoListController(ITodoListDatabaseService dbService)
    {
        this.dbService = dbService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetTodoListsCreatedByUserAsync(Guid userId)
    {
        var todoLists = await this.dbService.GetTodoListsCreatedByUserAsync(userId);
        return this.Ok(todoLists);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoListAsync([FromBody] TodoListPostModel todoList)
    {
        await this.dbService.CreateTodoListAsync(todoList);
        return this.Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodoListAsync([FromBody] TodoListPutModel todoList)
    {
        await this.dbService.UpdateTodoListAsync(todoList);
        return this.Ok();
    }

    [HttpDelete("{listId:guid}")]
    public async Task<IActionResult> DeleteTodoListAsync(Guid listId)
    {
        await this.dbService.DeleteTodoListAsync(listId);
        return this.Ok();
    }
}
