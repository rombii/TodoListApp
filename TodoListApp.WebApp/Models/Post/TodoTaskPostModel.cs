namespace TodoListApp.WebApi.Models
{
    using System;

    public class TodoTaskPostModel
    {
        public string Title { get; set; } = null!;

        public DateTime DueDate { get; set; }

        public Guid ListId { get; set; }
    }
}
