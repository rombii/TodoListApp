namespace TodoListApp.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Entities;

public class TodoListDbContext : DbContext
{
    public TodoListDbContext(DbContextOptions<TodoListDbContext> options)
        : base(options)
    {
    }

    public DbSet<TodoListEntity>? TodoList { get; set; }

    public DbSet<TodoTaskEntity>? TodoTask { get; set; }

    public DbSet<TaskCommentEntity>? TaskComment { get; set; }

    public DbSet<TaskTagEntity>? TaskTag { get; set; }

    public DbSet<TodoListRoleEntity>? ListRole { get; set; }

    public DbSet<RoleEntity>? Role { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskTagEntity>()
            .HasMany(tag => tag.Tasks)
            .WithMany(task => task.Tags)
            .UsingEntity<Dictionary<string, object>>(
                "TaskTagEntityTodoTaskEntity",
                right => right
                    .HasOne<TodoTaskEntity>()
                    .WithMany()
                    .HasForeignKey("TasksId")
                    .OnDelete(DeleteBehavior.Restrict),
                left => left
                    .HasOne<TaskTagEntity>()
                    .WithMany()
                    .HasForeignKey("TagsId")
                    .OnDelete(DeleteBehavior.Cascade));

        modelBuilder.Entity<RoleEntity>().HasData(
                    new RoleEntity
                    {
                        Id = Guid.NewGuid(),
                        Role = "Owner",
                    },
                    new RoleEntity
                    {
                        Id = Guid.NewGuid(),
                        Role = "Editor",
                    },
                    new RoleEntity
                    {
                        Id = Guid.NewGuid(),
                        Role = "Viewer",
                    });
    }
}
