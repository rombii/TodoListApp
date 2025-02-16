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

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="model">The login model.</param>
    /// <returns>The login response.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] TodoListUserLoginModel model)
    {
        var response = await this.service.Login(model);
        return this.Ok(response);
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="model">The registration model.</param>
    /// <returns>Ok result.</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] TodoListUserPostModel model)
    {
        await this.service.Register(model);
        return this.Ok();
    }

    /// <summary>
    /// Refreshes the access token.
    /// </summary>
    /// <param name="accessToken">The current access token.</param>
    /// <returns>The new access token.</returns>
    [HttpGet("token/{accessToken}")]
    public async Task<IActionResult> RefreshToken(string accessToken)
    {
        var token = await this.service.RefreshToken(accessToken);
        return this.Ok(token);
    }

    /// <summary>
    /// Logs out the authenticated user.
    /// </summary>
    /// <returns>Ok result.</returns>
    [Authorize]
    [HttpPut("logout")]
    public async Task<IActionResult> Logout()
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.service.Logout(userLogin);
        return this.Ok();
    }
}
