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

    public async Task<List<TodoTaskModel>> GetTasksForTodoListAsync(Guid listId)
    {
        var entities = await this.dbContext.TodoTask
            .Where(todoList => todoList.ListId == listId)
            .ToListAsync();

        return this.mapper.Map<List<TodoTaskModel>>(entities);
    }

    public async Task<TodoTaskWithCommentsModel?> GetTask(Guid id)
    {
        var task = await this.dbContext.TodoTask.FirstOrDefaultAsync(task => task.Id == id);

        return task == null ? null : this.mapper.Map<TodoTaskWithCommentsModel>(task);
    }

    public async Task CreateTodoTaskAsync(TodoTaskPostModel model)
    {
        var entity = this.mapper.Map<TodoTaskEntity>(model);
        entity.Id = Guid.NewGuid();
        entity.CreatedDate = DateTime.UtcNow;

        await this.dbContext.AddAsync(entity);
    }

    public async Task DeleteTodoTaskAsync(Guid id)
    {
        var task = await this.dbContext.TodoTask.FirstOrDefaultAsync(task => task.Id == id);

        if (task == null)
        {
            return;
        }

        this.dbContext.TodoTask.Remove(task);
    }

    public async Task UpdateTodoTaskAsync(TodoTaskPutModel model)
    {
        var entity = await this.dbContext.TodoTask.FirstOrDefaultAsync(task => task.Id == model.Id);

        if (entity == null)
        {
            return;
        }

        entity.Title = model.Title;
        entity.DueDate = model.DueDate;
        entity.IsCompleted = model.IsCompleted;

        await this.dbContext.SaveChangesAsync();
    }

}
