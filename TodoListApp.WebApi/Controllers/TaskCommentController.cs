namespace TodoListApp.WebApi.Controllers;
using TodoListApp.WebApi.Models.Put;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TodoListApp.WebApi.Services.Interfaces;
using System.Security.Claims;
using TodoListApp.WebApi.Models.Post;


[Authorize]
[ApiController]
[Route("/api/[controller]")]
public class TaskCommentController : ControllerBase
{
    private readonly ITaskCommentDatabaseService service;

    public TaskCommentController(ITaskCommentDatabaseService service)
    {
        this.service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddCommentToTask([FromBody] TaskCommentPostModel model)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.service.AddCommentAsync(model, userLogin);
        return this.NoContent();
    }

    [HttpDelete("{commentId:guid}")]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.service.RemoveComment(commentId, userLogin);
        return this.NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComment([FromBody] TaskCommentPutModel model)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.service.UpdateComment(model, userLogin);
        return this.NoContent();
    }
}
