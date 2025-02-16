namespace TodoListApp.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;

[Route("Error")]
public class ErrorController : Controller
{
    [Route("NotFound")]
    public new IActionResult NotFound()
    {
        return this.View();
    }

    [Route("SessionExpired")]
    public IActionResult SessionExpired()
    {
        this.TempData["Error"] = "Session has expired. Please log in again.";
        return this.RedirectToAction("Login", "Auth");
    }

    [Route("Server")]
    public IActionResult Server()
    {
        return this.View();
    }
}
