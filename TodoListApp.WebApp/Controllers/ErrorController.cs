namespace TodoListApp.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/NotFound")]
    public IActionResult NotFound()
    {
        return this.View();
    }

    [Route("Error/SessionExpired")]
    public IActionResult SessionExpired()
    {
        this.TempData["Error"] = "Session has expired. Please log in again.";
        return this.RedirectToAction("Login", "Auth");
    }

    [Route("Error/Server")]
    public IActionResult Server()
    {
        return this.View();
    }
}
