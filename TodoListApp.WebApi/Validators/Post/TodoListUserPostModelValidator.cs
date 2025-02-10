using FluentValidation;
using TodoListApp.WebApi.Models.Post;

public class TodoListUserPostModelValidator : AbstractValidator<TodoListUserPostModel>
{
    public TodoListUserPostModelValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .Length(3, 50).WithMessage("Login must be between 3 and 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");

        RuleFor(x => x.RepeatPassword)
            .Equal(x => x.Password).WithMessage("Password and RepeatPassword must match.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");
    }
}