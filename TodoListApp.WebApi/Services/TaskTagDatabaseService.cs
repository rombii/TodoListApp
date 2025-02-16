namespace TodoListApp.WebApi.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Data;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models.Post;

public class TaskTagDatabaseService : ITaskTagDatabaseService
{
    private readonly TodoListDbContext dbContext;
    private readonly IMapper mapper;

    public TaskTagDatabaseService(TodoListDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<TaskTagModel>> GetAllTagsForList(Guid listId, string? issuer)
    {
        if (issuer == null)
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        var list = await this.dbContext.TodoList!.Include(todoListEntity => todoListEntity.Tags)
            .FirstOrDefaultAsync(list => list.Id == listId);

        if (list == null)
        {
            throw new KeyNotFoundException("Not found");
        }

        var role = await this.dbContext.ListRole!.FirstOrDefaultAsync(role =>
            role.ListId == listId && role.User == issuer);

        if (role == null)
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        var tags = list.Tags;

        return this.mapper.Map<List<TaskTagModel>>(tags);
    }

    public async Task<List<TodoTaskModel>> GetAllTasksForTag(Guid tagId)
    {
        var tag = await this.dbContext.TaskTag!.FirstOrDefaultAsync(tag => tag.Id == tagId);
        if (tag == null)
        {
            throw new KeyNotFoundException("Not found");
        }

        var tasks = await this.dbContext.TodoTask!.Where(task => task.Tags.Contains(tag)).ToListAsync();

        return this.mapper.Map<List<TodoTaskModel>>(tasks);
    }

    public async Task<List<TaskTagModel>> GetAllTagsForTask(Guid taskId)
    {
        var task = await this.dbContext.TodoTask!.FirstOrDefaultAsync(task => task.Id == taskId);
        if (task == null)
        {
            throw new KeyNotFoundException("Not found");
        }

        var tags = await this.dbContext.TaskTag!.Where(tag => tag.Tasks.Contains(task)).ToListAsync();

        return this.mapper.Map<List<TaskTagModel>>(tags);
    }

    public async Task AddTag(TaskTagPostModel model, string? issuer)
    {
        if (issuer == null)
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        var tag = this.mapper.Map<TaskTagEntity>(model);

        var role = await this.dbContext.ListRole!.Include(todoListRoleEntity => todoListRoleEntity.Role).FirstOrDefaultAsync(role =>
            role.ListId == model.ListId && role.User == issuer);

        if (role == null || role.Role.Role == "Viewer")
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        await this.dbContext.TaskTag!.AddAsync(tag);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task AssignTag(Guid tagId, Guid taskId, string? issuer)
    {
        var task = await this.dbContext.TodoTask!.Include(task => task.Tags)
            .Include(todoTaskEntity => todoTaskEntity.List).FirstOrDefaultAsync(task => task.Id == taskId);
        var tag = await this.dbContext.TaskTag!.FirstOrDefaultAsync(tag => tag.Id == tagId);
        if (task == null || tag == null)
        {
            throw new KeyNotFoundException("Not found");
        }

        var role = await this.dbContext.ListRole!.Include(todoListRoleEntity => todoListRoleEntity.Role).FirstOrDefaultAsync(
            role => role.ListId == task.ListId && role.User == issuer);

        if (issuer == null || role == null || role.Role.Role == "Viewer" || tag.ListId != task.ListId)
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        if (!task.Tags.Contains(tag))
        {
            task.Tags.Add(tag);
        }

        await this.dbContext.SaveChangesAsync();
    }

    public async Task RemoveTag(Guid tagId, Guid taskId, string? issuer)
    {
        var task = await this.dbContext.TodoTask!.Include(task => task.Tags)
            .Include(todoTaskEntity => todoTaskEntity.List).FirstOrDefaultAsync(task => task.Id == taskId);
        var tag = await this.dbContext.TaskTag!.FirstOrDefaultAsync(tag => tag.Id == tagId);
        if (task == null || tag == null)
        {
            throw new KeyNotFoundException("Not found");
        }

        var role = await this.dbContext.ListRole!.Include(todoListRoleEntity => todoListRoleEntity.Role).FirstOrDefaultAsync(
            role => role.ListId == task.ListId && role.User == issuer);

        if (issuer == null || role == null || role.Role.Role == "Viewer" || tag.ListId != task.ListId)
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        if (task.Tags.Contains(tag))
        {
            task.Tags.Remove(tag);
        }

        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteTag(Guid tagId, string? issuer)
    {
        var tag = await this.dbContext.TaskTag!.FirstOrDefaultAsync(tag => tag.Id == tagId);

        if (tag == null)
        {
            return; // If tag doesn't exist endpoint achieved final state (tag is gone)
        }

        var role = await this.dbContext.ListRole!.Include(todoListRoleEntity => todoListRoleEntity.Role).FirstOrDefaultAsync(
            role => role.ListId == tag.ListId && role.User == issuer);

        if (issuer == null || role == null || role.Role.Role == "Viewer")
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        this.dbContext.TaskTag!.Remove(tag);
        await this.dbContext.SaveChangesAsync();
    }
}
