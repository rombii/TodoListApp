﻿namespace TodoListApp.WebApi.Models.Put;

public class TaskCommentPutModel
{
    public Guid Id { get; set; }

    public string Comment { get; set; }
}
