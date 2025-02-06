using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Data;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Put;
using TodoListApp.WebApi.Services.Interfaces;

public class TodoListDatabaseService : ITodoListDatabaseService
{
    private readonly TodoListDbContext dbContext;

    public TodoListDatabaseService(TodoListDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<TodoListModel>> GetTodoListsCreatedByUserAsync(Guid userId)
    {
        var entities = await this.dbContext.TodoList
            .Where(todoList => todoList.CreatedBy == userId)
            .ToListAsync();

        var tasks = entities.Select(MapToModelAsync);
        return (await Task.WhenAll(tasks)).ToList();
    }

    public async Task CreateTodoListAsync(TodoListModel todoList)
    {
        var entity = new TodoListEntity
        {
            Id = Guid.NewGuid(),
            Title = todoList.Title,
            Description = todoList.Description,
            CreatedBy = todoList.CreatedBy,
            CreatedDate = DateTime.UtcNow,
        };

        await this.dbContext.TodoList.AddAsync(entity);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateTodoListAsync(TodoListPutModel todoList)
    {
        var entity = await this.dbContext.TodoList
            .Where(todoListEntity => todoListEntity.Id == todoList.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new Exception("Todo list not found");
        }

        entity.Title = todoList.Title;
        entity.Description = todoList.Description;

        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteTodoListAsync(Guid listId)
    {
        var entity = await this.dbContext.TodoList
            .Where(todoListEntity => todoListEntity.Id == listId)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new Exception("Todo list not found");
        }

        this.dbContext.TodoList.Remove(entity);
        await this.dbContext.SaveChangesAsync();
    }

    private static async Task<TodoListModel> MapToModelAsync(TodoListEntity entity)
    {
        return await Task.Run(() => new TodoListModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            CreatedBy = entity.CreatedBy,
            CreatedDate = entity.CreatedDate,
        });
    }
}
