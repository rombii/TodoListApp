namespace TodoListApp.WebApi.Data;
using Microsoft.EntityFrameworkCore;

public class TodoListUserDbContext : DbContext
{
    public TodoListUserDbContext(DbContextOptions<TodoListUserDbContext> options)
        : base(options)
    {
    }
}
