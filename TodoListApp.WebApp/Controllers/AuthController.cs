namespace TodoListApp.WebApp.Controllers;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Models.Post;
using TodoListApp.WebApp.Services;


public class AuthController : Controller
{
    private readonly AuthService authService;

    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (this.ModelState.IsValid)
        {
            var token = await this.authService.LoginAsync(model);

            if (token != null)
            {
                this.HttpContext.Session.SetString("AccessToken", token);
                return this.RedirectToAction("Index", "Home");
            }

            this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return this.View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(TodoListUserPostModel model)
    {
        if (this.ModelState.IsValid)
        {
            var response = await this.authService.RegisterAsync(model);

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return this.RedirectToAction("Login");
            }

            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                var errorResponse = await response.Content.ReadAsStringAsync();
                var errors = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(errorResponse);

                foreach (var error in errors)
                {
                    var errorMessages = string.Join("<br />", error.Value);
                    this.ModelState.AddModelError(error.Key, errorMessages);
                }
            }
            else
            {
                this.ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            }
        }

        return this.View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await this.authService.LogoutAsync();
        this.HttpContext.Session.Remove("AccessToken");
        return this.RedirectToAction("Login");
    }
}
