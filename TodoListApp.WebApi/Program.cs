#pragma warning disable SA1200
using Microsoft.EntityFrameworkCore;
using TodoListApp.WebApi.Data;
using TodoListApp.WebApi.Services.Interfaces;
#pragma warning restore SA1200

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TodoListDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:TodoListConnection"]);
});
builder.Services.AddDbContext<TodoListUserDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:TodoListUserConnection"]);
});

builder.Services.AddScoped<ITodoListDatabaseService, TodoListDatabaseService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
