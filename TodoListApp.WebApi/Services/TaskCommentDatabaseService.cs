namespace TodoListApp.WebApi.Services;
using TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models.Put;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Data;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;



public class TaskCommentDatabaseService : ITaskCommentDatabaseService
{
    private readonly TodoListDbContext dbContext;
    private readonly IMapper mapper;

    public TaskCommentDatabaseService(TodoListDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task AddCommentAsync(TaskCommentPostModel model, string? issuer)
    {
        var task = await this.dbContext.TodoTask.FirstOrDefaultAsync(task => task.Id == model.TaskId);
        if (task == null)
        {
            throw new KeyNotFoundException("Task not found");
        }

        if (issuer == null || !await this.dbContext.ListRole.AnyAsync(role => role.ListId == task.ListId && role.User == issuer))
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        var entity = this.mapper.Map<TaskCommentEntity>(model);

        await this.dbContext.TaskComment.AddAsync(entity);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task RemoveComment(Guid commentId, string? issuer)
    {
        var comment = await this.dbContext.TaskComment.Include(c => c.Task)
            .FirstOrDefaultAsync(c => c.Id == commentId);
        if (comment == null)
        {
            throw new KeyNotFoundException("Comment not found");
        }

        if (issuer == null || !await this.dbContext.ListRole.AnyAsync(role =>
            role.ListId == comment.Task.ListId && role.User == issuer))
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        this.dbContext.TaskComment.Remove(comment);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateComment(TaskCommentPutModel model, string? issuer)
    {
        var comment = await this.dbContext.TaskComment.Include(c => c.Task)
            .FirstOrDefaultAsync(c => c.Id == model.Id);
        if (comment == null)
        {
            throw new KeyNotFoundException("Comment not found");
        }

        if (issuer == null || !await this.dbContext.ListRole.AnyAsync(role =>
                role.ListId == comment.Task.ListId && role.User == issuer))
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        comment.Comment = model.Comment;
        await this.dbContext.SaveChangesAsync();
    }
}
