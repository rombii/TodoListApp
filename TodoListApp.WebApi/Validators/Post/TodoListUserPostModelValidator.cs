namespace TodoListApp.WebApi.Validators.Post;
using FluentValidation;
using TodoListApp.WebApi.Models.Post;

public class TodoListUserPostModelValidator : AbstractValidator<TodoListUserPostModel>
{
    public TodoListUserPostModelValidator()
    {
        this.RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .Length(3, 50).WithMessage("Login must be between 3 and 50 characters.");

        this.RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

        this.RuleFor(x => x.RepeatPassword)
            .Equal(x => x.Password).WithMessage("Passwords must match.");

        this.RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");
    }
}
