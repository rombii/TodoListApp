namespace TodoListApp.WebApi.Validators.Post;
using FluentValidation;
using TodoListApp.WebApi.Models.Post;

public class TodoTaskPostModelValidator : AbstractValidator<TodoTaskPostModel>
{
    public TodoTaskPostModelValidator()
    {
        this.RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

        this.RuleFor(x => x.DueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("DueDate cannot be in the past.");

        this.RuleFor(x => x.ListId)
            .NotEmpty().WithMessage("ListId is required.")
            .NotEqual(Guid.Empty).WithMessage("ListId must be a valid GUID.");
    }
}
