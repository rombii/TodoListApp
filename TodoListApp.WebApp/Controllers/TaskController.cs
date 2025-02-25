﻿namespace TodoListApp.WebApp.Controllers;
using System.Net;
using TodoListApp.WebApp.Services.Interfaces;
using TodoListApp.WebApp.Models;
using TodoListApp.WebApp.Models.Post;
using TodoListApp.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApp.Models.Put;

public class TaskController : Controller
{
    private readonly ITodoTaskService taskService;
    private readonly ITagService tagService;

    public TaskController(ITodoTaskService taskService, ITagService tagService)
    {
        this.taskService = taskService;
        this.tagService = tagService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(Guid? todoListId)
    {
        if (this.ModelState.IsValid)
        {
            try
            {
                if (todoListId.HasValue)
                {
                    var tasks = await this.taskService.GetTasksForTodoListAsync(todoListId.Value);
                    this.ViewBag.TodoListId = todoListId.Value;
                    var tags = await this.tagService.GetTagsForListAsync(todoListId.Value);
                    this.ViewBag.Tags = tags;
                    return this.View(tasks);
                }
                else
                {
                    var tasks = await this.taskService.GetTasksForUserAsync();
                    return this.View(tasks);
                }
            }
            catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
            {
                return this.RedirectToAction("Login", "Auth");
            }
        }

        return this.RedirectToAction("Index", "TodoList");
    }

    [HttpGet]
    public async Task<IActionResult> Overdue()
    {
        try
        {
            var tasks = await this.taskService.GetOverdueTasksAsync();
            this.ViewBag.IsOverdue = true;
            return this.View("Index", tasks);
        }
        catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.Unauthorized)
        {
            return this.RedirectToAction("Login", "Auth");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(TodoTaskPostModel task)
    {
        if (this.ModelState.IsValid)
        {
            await this.taskService.CreateTaskAsync(task);
        }

        return this.RedirectToAction("Index", new { todoListId = task.ListId });
    }

    [HttpPost]
    public async Task<IActionResult> Update(TodoTaskPutModel task)
    {
        if (this.ModelState.IsValid)
        {
            await this.taskService.UpdateTaskAsync(task);
        }

        return this.RedirectToAction("Index", new { todoListId = task.ListId });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(Guid taskId, Guid listId)
    {
        if (this.ModelState.IsValid)
        {
            await this.taskService.DeleteTaskAsync(taskId);
        }

        return this.RedirectToAction("Index", new { todoListId = listId });
    }

    [HttpPost]
    public async Task<IActionResult> ChangeStatus(Guid taskId, Guid? listId, bool isOverdue)
    {
        if (this.ModelState.IsValid)
        {
            await this.taskService.ChangeTaskStatusAsync(taskId);
        }

        if (isOverdue)
        {
            return this.RedirectToAction("Overdue");
        }

        return this.RedirectToAction("Index", new { todoListId = listId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTagToList(string tagName, Guid listId)
    {
        if (string.IsNullOrEmpty(tagName))
        {
            this.ModelState.AddModelError("TagName", "Tag name is required.");
            return this.RedirectToAction("Index", new { listId });
        }

        var tag = new TaskTagPostModel
        {
            Tag = tagName,
            ListId = listId,
        };

        await this.tagService.AddTagAsync(tag);

        return this.RedirectToAction("Index", new { todoListId = listId });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RemoveTagFromList(Guid tagId, Guid listId)
    {
        await this.tagService.RemoveTagAsync(tagId);
        return this.RedirectToAction("Index", new { todoListId = listId });
    }
}
