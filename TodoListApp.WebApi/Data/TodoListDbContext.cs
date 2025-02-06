namespace TodoListApp.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Entities;

public class TodoListDbContext : DbContext
{
    public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
        : base(options)
    {
    }

    public DbSet<TodoListEntity> TodoList { get; set; }

    public DbSet<TodoTaskEntity> TodoTask { get; set; }

    public DbSet<TaskCommentEntity> TaskComment { get; set; }

    public DbSet<TaskTagEntity> TaskTag { get; set; }
}
