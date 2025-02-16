namespace TodoListApp.WebApp.Controllers;
using System.Text.Json;
using TodoListApp.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApp.Models.Post;
using TodoListApp.WebApp.Models.Put;
using TodoListApp.WebApp.Services;

public class TaskDetailsController : Controller
{
    private readonly ICommentService commentService;
    private readonly ITagService tagService;
    private readonly ITodoTaskService taskService;

    public TaskDetailsController(ICommentService commentService, ITagService tagService, ITodoTaskService taskService)
    {
        this.commentService = commentService;
        this.tagService = tagService;
        this.taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid taskId)
    {
        if (this.ModelState.IsValid)
        {
            var task = await this.taskService.GetTaskAsync(taskId);
            return this.View(task);
        }

        return this.RedirectToAction("Index", "TodoList");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(TaskCommentPostModel model)
    {
        if (this.ModelState.IsValid)
        {
            var response = await this.commentService.AddCommentAsync(model);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Index", new { taskId = model.TaskId });
            }
        }

        this.TempData["ErrorMessage"] = "Failed to add comment.";
        return this.RedirectToAction("Index", new { taskId = model.TaskId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditComment(Guid commentId, string comment, Guid taskId)
    {
        if (this.ModelState.IsValid)
        {
            var response = await this.commentService.EditCommentAsync(new TaskCommentPutModel
            {
                Id = commentId,
                Comment = comment,
            });
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Index", new { taskId });
            }
        }

        this.TempData["ErrorMessage"] = "Failed to edit comment.";
        return this.RedirectToAction("Index", new { taskId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteComment(Guid commentId, Guid taskId)
    {
        if (this.ModelState.IsValid)
        {
            var response = await this.commentService.DeleteCommentAsync(commentId);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Index", new { taskId });
            }
        }

        this.TempData["ErrorMessage"] = "Failed to delete comment.";
        return this.RedirectToAction("Index", new { taskId });
    }

    [HttpGet]
    public async Task<IActionResult> ShowTagPicker(Guid listId, Guid taskId)
    {
        if (this.ModelState.IsValid)
        {
            var availableTags = await this.tagService.GetTagsForListAsync(listId);

            this.TempData["Tags"] = JsonSerializer.Serialize(availableTags);
        }

        return this.RedirectToAction("Index", new { taskId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AssignTag(Guid taskId, Guid tagId)
    {
        if (this.ModelState.IsValid)
        {
            var response = await this.tagService.AssignTagToTaskAsync(taskId, tagId);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Index", new { taskId });
            }
        }

        this.TempData["ErrorMessage"] = "Failed to assign tag.";
        return this.RedirectToAction("Index", new { taskId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveTag(Guid taskId, Guid tagId)
    {
        if (this.ModelState.IsValid)
        {
            var response = await this.tagService.RemoveTagFromTaskAsync(taskId, tagId);
            if (response.IsSuccessStatusCode)
            {
                return this.RedirectToAction("Index", new { taskId });
            }
        }

        this.TempData["ErrorMessage"] = "Failed to remove tag.";
        return this.RedirectToAction("Index", new { taskId });
    }
}
