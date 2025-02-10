using FluentValidation;
using TodoListApp.WebApi.Models;

public class TodoListUserLoginModelValidator : AbstractValidator<TodoListUserLoginModel>
{
    public TodoListUserLoginModelValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .Length(3, 50).WithMessage("Login must be between 3 and 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}