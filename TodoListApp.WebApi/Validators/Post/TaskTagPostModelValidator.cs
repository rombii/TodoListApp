namespace TodoListApp.WebApi.Validators.Post;
using FluentValidation;
using TodoListApp.WebApi.Models.Post;

public class TaskTagPostModelValidator : AbstractValidator<TaskTagPostModel>
{
    public TaskTagPostModelValidator()
    {
        this.RuleFor(x => x.Tag)
            .NotEmpty().WithMessage("Tag is required.")
            .MaximumLength(50).WithMessage("Tag cannot exceed 50 characters.");

        this.RuleFor(x => x.ListId)
            .NotEmpty().WithMessage("ListId is required.")
            .NotEqual(Guid.Empty).WithMessage("ListId must be a valid GUID.");
    }
}
