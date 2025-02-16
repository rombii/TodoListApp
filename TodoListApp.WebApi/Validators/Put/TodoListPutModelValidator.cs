namespace TodoListApp.WebApi.Validators.Put;
using FluentValidation;
using TodoListApp.WebApi.Models.Put;

public class TodoListPutModelValidator : AbstractValidator<TodoListPutModel>
{
    public TodoListPutModelValidator()
    {
        this.RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Id must be a valid GUID.");

        this.RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

        this.RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");
    }
}
