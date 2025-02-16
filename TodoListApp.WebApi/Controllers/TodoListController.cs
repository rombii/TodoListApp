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

    /// <summary>
    /// Gets all todolists for the authenticated user.
    /// </summary>
    /// <returns>A list of todolists.</returns>
    [HttpGet]
    public async Task<IActionResult> GetTodoListsUserAsync()
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var todoLists = await this.dbService.GetTodoListsForUserAsync(userLogin);
        return this.Ok(todoLists);
    }

    /// <summary>
    /// Creates a new todolist.
    /// </summary>
    /// <param name="todoList">The new todolist model.</param>
    /// <returns>No content.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateTodoListAsync([FromBody] TodoListPostModel todoList)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.dbService.CreateTodoListAsync(todoList, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Updates an existing todolist.
    /// </summary>
    /// <param name="todoList">The updated todolist model.</param>
    /// <returns>No content.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTodoListAsync([FromBody] TodoListPutModel todoList)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.dbService.UpdateTodoListAsync(todoList, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Deletes a todolist by its ID.
    /// </summary>
    /// <param name="listId">The ID of the todolist to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{listId:guid}")]
    public async Task<IActionResult> DeleteTodoListAsync(Guid listId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.dbService.DeleteTodoListAsync(listId, userLogin);
        return this.NoContent();
    }
}
