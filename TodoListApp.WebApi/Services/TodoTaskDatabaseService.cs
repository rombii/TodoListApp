using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Data;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;
using TodoListApp.WebApi.Models.Put;
using TodoListApp.WebApi.Profiles;
using TodoListApp.WebApi.Services.Interfaces;

public class TodoTaskDatabaseService : ITodoTaskDatabaseService
{
    private readonly TodoListDbContext dbContext;
    private readonly IMapper mapper;

    public TodoTaskDatabaseService(TodoListDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<List<TodoTaskModel>> GetTasksForTodoListAsync(Guid listId, string? issuer)
    {
        if (!await this.dbContext.ListRole.AnyAsync(role => role.ListId == listId && role.User == issuer))
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        if (!await this.dbContext.TodoList.AnyAsync(list => list.Id == listId))
        {
            throw new KeyNotFoundException("Not found");
        }

        var entities = await this.dbContext.TodoTask
            .Where(task => task.ListId == listId)
            .Include(task => task.Tags)
            .ToListAsync();

        return this.mapper.Map<List<TodoTaskModel>>(entities);
    }

    public async Task<TodoTaskWithCommentsModel> GetTask(Guid id, string? issuer)
    {
        var task = await this.dbContext.TodoTask.Include(task => task.Comments).FirstOrDefaultAsync(task => task.Id == id &&
                                                                             this.dbContext.ListRole.Any(role => role.ListId == task.ListId && role.User == issuer));

        if (task == null)
        {
            throw new KeyNotFoundException("Unauthorized");
        }

        return this.mapper.Map<TodoTaskWithCommentsModel>(task);
    }

    public async Task CreateTodoTaskAsync(TodoTaskPostModel model, string? issuer)
    {
        var list = await this.dbContext.TodoList.FirstOrDefaultAsync(list => list.Id == model.ListId);

        if (list == null)
        {
            throw new KeyNotFoundException("Not found");
        }

        var role = await this.dbContext.ListRole.Include(todoListRoleEntity => todoListRoleEntity.Role).FirstOrDefaultAsync(role =>
            role.ListId == list.Id && role.User == issuer);

        if (role == null || role.Role.Role != "Owner")
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        var entity = this.mapper.Map<TodoTaskEntity>(model);
        entity.Id = Guid.NewGuid();
        entity.CreatedDate = DateTime.UtcNow;

        await this.dbContext.AddAsync(entity);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteTodoTaskAsync(Guid id, string? issuer)
    {
        var task = await this.dbContext.TodoTask
            .Include(todoTaskEntity => todoTaskEntity.List)
            .FirstOrDefaultAsync(task => task.Id == id);

        if (task == null)
        {
            return;
        }

        var role = await this.dbContext.ListRole.Include(todoListRoleEntity => todoListRoleEntity.Role).FirstOrDefaultAsync(role =>
            role.ListId == task.ListId && role.User == issuer);

        if (role == null || role.Role.Role != "Owner")
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        this.dbContext.TodoTask.Remove(task);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateTodoTaskAsync(TodoTaskPutModel model, string? issuer)
    {
        var entity = await this.dbContext.TodoTask
            .Include(todoTaskEntity => todoTaskEntity.List)
            .FirstOrDefaultAsync(task => task.Id == model.Id);

        if (entity == null)
        {
            return;
        }

        var role = await this.dbContext.ListRole.Include(todoListRoleEntity => todoListRoleEntity.Role).FirstOrDefaultAsync(role =>
            role.ListId == entity.ListId && role.User == issuer);

        if (role == null || role.Role.Role != "Owner")
        {
            throw new UnauthorizedAccessException("Unauthorized");
        }

        entity.Title = model.Title;
        entity.DueDate = model.DueDate;
        entity.IsCompleted = model.IsCompleted;

        await this.dbContext.SaveChangesAsync();
    }

    public async Task<List<TodoTaskModel>> GetOverdueTasksForUserAsync(string? issuer)
    {
        return null;
    }

}
