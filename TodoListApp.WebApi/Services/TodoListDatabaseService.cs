namespace TodoListApp.WebApi.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Data;
using TodoListApp.WebApi.Entities;
using TodoListApp.WebApi.Models;
using TodoListApp.WebApi.Models.Post;
using TodoListApp.WebApi.Models.Put;
using TodoListApp.WebApi.Services.Interfaces;


public class TodoListDatabaseService : ITodoListDatabaseService
{
    private readonly TodoListDbContext listDbContext;
    private readonly TodoListUserDbContext userDbContext;
    private readonly IMapper mapper;

    public TodoListDatabaseService(TodoListDbContext listDbContext, TodoListUserDbContext userDbContext, IMapper mapper)
    {
        this.listDbContext = listDbContext;
        this.userDbContext = userDbContext;
        this.mapper = mapper;
    }

    public async Task<List<TodoListModel>> GetTodoListsForUserAsync(string? issuer)
    {
        if (issuer == null)
        {
            throw new UnauthorizedAccessException();
        }

        var entities = await this.listDbContext.TodoList
            .Where(todoList => this.listDbContext.ListRole
                .Any(role => role.ListId == todoList.Id && role.User == issuer))
            .ToListAsync();


        return this.mapper.Map<List<TodoListModel>>(entities);
    }

    public async Task CreateTodoListAsync(TodoListPostModel todoList, string? issuer)
    {
        this.ValidateUser(issuer);
        var listGuid = Guid.NewGuid();
        var entity = new TodoListEntity
        {
            Id = listGuid,
            Title = todoList.Title,
            Description = todoList.Description,
            CreatedDate = DateTime.UtcNow,
        };

        var role = new TodoListRoleEntity
        {
            Id = Guid.NewGuid(),
            User = issuer,
            ListId = listGuid,
            RoleId = (await this.listDbContext.Role.FirstAsync(role => role.Role == "Owner")).Id,
        };

        await this.listDbContext.TodoList.AddAsync(entity);
        await this.listDbContext.ListRole.AddAsync(role);
        await this.listDbContext.SaveChangesAsync();
    }

    public async Task UpdateTodoListAsync(TodoListPutModel todoList, string? issuer)
    {
        var entity = await this.listDbContext.TodoList
            .Where(todoListEntity => todoListEntity.Id == todoList.Id)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new KeyNotFoundException("Todo list not found");
        }

        if (issuer == null)
        {
            throw new UnauthorizedAccessException();
        }

        var role = await this.listDbContext.ListRole.Include(todoListRoleEntity => todoListRoleEntity.Role).FirstOrDefaultAsync(role =>
            role.ListId == entity.Id && role.User == issuer);

        if (role == null || role.Role.Role != "Owner")
        {
            throw new UnauthorizedAccessException();
        }

        entity.Title = todoList.Title;
        entity.Description = todoList.Description;

        await this.listDbContext.SaveChangesAsync();
    }

    public async Task DeleteTodoListAsync(Guid listId, string? issuer)
    {
        var entity = await this.listDbContext.TodoList
            .Where(todoListEntity => todoListEntity.Id == listId)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            throw new KeyNotFoundException("Todo list not found");
        }

        if (issuer == null)
        {
            throw new UnauthorizedAccessException();
        }

        var role = await this.listDbContext.ListRole.Include(todoListRoleEntity => todoListRoleEntity.Role).FirstOrDefaultAsync(role =>
            role.ListId == entity.Id && role.User == issuer);

        if (role == null || role.Role.Role != "Owner")
        {
            throw new UnauthorizedAccessException();
        }

        this.listDbContext.TodoList.Remove(entity);
        await this.listDbContext.SaveChangesAsync();
    }

    private void ValidateUser(string? issuer)
    {
        if (issuer == null)
        {
            throw new UnauthorizedAccessException();
        }

        var user = this.userDbContext.Users.FirstOrDefaultAsync(user => user.Login == issuer);

        if (user == null)
        {
            throw new UnauthorizedAccessException();
        }
    }
}
