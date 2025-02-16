namespace TodoListApp.WebApi.Validators.Put;
using FluentValidation;
using TodoListApp.WebApi.Models.Put;

public class TodoTaskPutModelValidator : AbstractValidator<TodoTaskPutModel>
{
    public TodoTaskPutModelValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");

        this.RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

        this.RuleFor(x => x.IsCompleted)
            .NotNull().WithMessage("IsCompleted must not be null.");

        this.RuleFor(x => x.DueDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("DueDate cannot be in the past.");
    }
}
