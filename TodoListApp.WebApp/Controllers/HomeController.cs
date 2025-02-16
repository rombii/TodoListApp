namespace TodoListApp.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{
    public Task<IActionResult> Index()
    {
        return Task.FromResult<IActionResult>(this.View());
    }
}
