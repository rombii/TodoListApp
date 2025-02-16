namespace TodoListApp.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Entities;

public class TodoListUserDbContext : DbContext
{
    public TodoListUserDbContext(DbContextOptions<TodoListUserDbContext> options)
        : base(options)
    {
    }

    public DbSet<TodoListUserEntity>? Users { get; set; }
}
