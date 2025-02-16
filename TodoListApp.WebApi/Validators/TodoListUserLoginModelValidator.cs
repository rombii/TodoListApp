namespace TodoListApp.WebApi.Validators;
using FluentValidation;
using TodoListApp.WebApi.Models;

public class TodoListUserLoginModelValidator : AbstractValidator<TodoListUserLoginModel>
{
    public TodoListUserLoginModelValidator()
    {
        this.RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .Length(3, 50).WithMessage("Login must be between 3 and 50 characters.");

        this.RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
