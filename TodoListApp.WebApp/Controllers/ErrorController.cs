using Microsoft.AspNetCore.Mvc;

public class ErrorController : Controller
{
    [Route("Error/NotFound")]
    public IActionResult NotFound()
    {
        return View();
    }

    [Route("Error/SessionExpired")]
    public IActionResult SessionExpired()
    {
        TempData["Error"] = "Session has expired. Please log in again.";
        return RedirectToAction("Login", "Account");
    }

    [Route("Error/Internal")]
    public IActionResult Internal()
    {
        return View();
    }
}
