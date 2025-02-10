namespace TodoListApp.WebApi.Controllers;
using System.Security.Claims;
using TodoListApp.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Put;
using TodoListApp.WebApi.Models.Post;

[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class TodoListController : ControllerBase
{
    private readonly ITodoListDatabaseService dbService;

    public TodoListController(ITodoListDatabaseService dbService)
    {
        this.dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTodoListsCreatedByUserAsync()
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var todoLists = await this.dbService.GetTodoListsCreatedForUserAsync(userLogin);
        return this.Ok(todoLists);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoListAsync([FromBody] TodoListPostModel todoList)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.dbService.CreateTodoListAsync(todoList, userLogin);
        return this.NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTodoListAsync([FromBody] TodoListPutModel todoList)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.dbService.UpdateTodoListAsync(todoList, userLogin);
        return this.NoContent();
    }

    [HttpDelete("{listId:guid}")]
    public async Task<IActionResult> DeleteTodoListAsync(Guid listId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.dbService.DeleteTodoListAsync(listId, userLogin);
        return this.NoContent();
    }
}
