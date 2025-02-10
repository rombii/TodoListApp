﻿namespace TodoListApp.WebApi.Models.Post;

public class TodoTaskPostModel
{
    public string Title { get; set; }

    public DateTime DueDate { get; set; }

    public Guid ListId { get; set; }
}
