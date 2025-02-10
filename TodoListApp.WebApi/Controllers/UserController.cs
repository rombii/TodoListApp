namespace TodoListApp.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TodoListApp.WebApi.Generators;
using TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;



[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ITodoListUserDatabaseService service;

    public UserController(ITodoListUserDatabaseService service)
    {
        this.service = service;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] TodoListUserLoginModel model)
    {
        var response = await this.service.Login(model);
        return this.Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] TodoListUserPostModel model)
    {
        await this.service.Register(model);
        return this.Ok();
    }

    [HttpGet("token/{accessToken}")]
    public async Task<IActionResult> RefreshToken(string accessToken)
    {
        var token = await this.service.RefreshToken(accessToken);
        return this.Ok(token);
    }

    [Authorize]
    [HttpPut("logout")]
    public async Task<IActionResult> Logout()
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.service.Logout(userLogin);
        return this.Ok();
    }

    // TODO
    // reset password
}
