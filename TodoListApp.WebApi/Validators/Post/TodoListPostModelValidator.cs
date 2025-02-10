using FluentValidation;
using TodoListApp.WebApi.Models.Post;

public class TodoListPostModelValidator : AbstractValidator<TodoListPostModel>
{
    public TodoListPostModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
    }
}