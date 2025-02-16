namespace TodoListApp.WebApp.Controllers;
using TodoListApp.WebApp.Services;
using TodoListApp.WebApp.Services.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApp.Models.Post;
using TodoListApp.WebApp.Models.Put;

public class TodoListController : Controller
{
    private readonly ITodoListService todoListService;

    public TodoListController(ITodoListService todoListService)
    {
        this.todoListService = todoListService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var todoLists = await this.todoListService.GetTodoListsAsync();
            return this.View(todoLists);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            return await this.RedirectToLogin();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(TodoListPostModel todoList)
    {
        if (this.ModelState.IsValid)
        {
            try
            {
                await this.todoListService.CreateTodoListAsync(todoList);
                return this.RedirectToAction("Index");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await this.RedirectToLogin();
            }
        }

        return this.RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Update(TodoListPutModel todoList)
    {
        if (this.ModelState.IsValid)
        {
            try
            {
                await this.todoListService.UpdateTodoListAsync(todoList);
                return this.RedirectToAction("Index");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await this.RedirectToLogin();
            }
        }

        return this.View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid listId)
    {
        if (this.ModelState.IsValid)
        {
            try
            {
                await this.todoListService.DeleteTodoListAsync(listId);
                return this.RedirectToAction("Index");
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                return await this.RedirectToLogin();
            }
        }

        return this.View("Index");
    }

    private Task<IActionResult> RedirectToLogin()
    {
        return Task.FromResult<IActionResult>(this.HttpContext.Session.TryGetValue("AccessToken", out _) ?
            this.RedirectToAction("Index", "Home") :
            this.RedirectToAction("Login", "Auth"));
    }
}
