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

    /// <summary>
    /// Adds a comment to a task.
    /// </summary>
    /// <param name="model">The comment model.</param>
    /// <returns>No content.</returns>
    [HttpPost]
    public async Task<IActionResult> AddCommentToTask([FromBody] TaskCommentPostModel model)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.service.AddCommentAsync(model, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Deletes a comment by its ID.
    /// </summary>
    /// <param name="commentId">The ID of the comment to delete.</param>
    /// <returns>No content.</returns>
    [HttpDelete("{commentId:guid}")]
    public async Task<IActionResult> DeleteComment(Guid commentId)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.service.RemoveComment(commentId, userLogin);
        return this.NoContent();
    }

    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    /// <param name="model">The updated comment model.</param>
    /// <returns>No content.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateComment([FromBody] TaskCommentPutModel model)
    {
        var userLogin = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await this.service.UpdateComment(model, userLogin);
        return this.NoContent();
    }
}
