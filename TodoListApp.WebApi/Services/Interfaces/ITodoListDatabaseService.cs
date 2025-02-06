﻿namespace TodoListApp.WebApi.Services.Interfaces;
using TodoListApp.WebApi.Models.Put;

using TodoListApp.WebApi.Models;
public interface ITodoListDatabaseService
{
    Task<List<TodoListModel>> GetTodoListsCreatedByUserAsync(Guid userId);

    Task CreateTodoListAsync(TodoListModel todoList);

    Task UpdateTodoListAsync(TodoListPutModel todoList);

    Task DeleteTodoListAsync(Guid listId);
}
