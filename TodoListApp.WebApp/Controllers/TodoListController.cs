namespace TodoListApp.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Services.Interfaces;

[Route("TodoList")]
public class TodoListController : Controller
{
    private readonly ITodoListWebApiService webApiService;

    public TodoListController(ITodoListWebApiService webApiService)
    {
        this.webApiService = webApiService;
    }

    public async Task<IActionResult> Index()
    {
        var todoLists = await this.webApiService.GetTodoListsCreatedByUserAsync(Guid.Empty);
        return this.View(todoLists);
    }

    [Route("Create")]
    public IActionResult Create()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] TodoListWebApiModel todoList)
    {
        await this.webApiService.CreateTodoListAsync(todoList);
        return this.RedirectToAction("Index");
    }

    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
       await this.webApiService.DeleteTodoListAsync(id);
       return this.RedirectToAction(nameof(this.Index));
    }

    [HttpPost]
    public async Task<IActionResult> Update(TodoListWebApiModel todoList)
    {
       if (!ModelState.IsValid)
       {
           return View(todoList);
       }

       await this.webApiService.UpdateTodoListAsync(todoList);
       return this.RedirectToAction(nameof(this.Index));
   }
}
