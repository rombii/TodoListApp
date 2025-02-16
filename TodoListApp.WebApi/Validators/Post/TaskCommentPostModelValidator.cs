namespace TodoListApp.WebApi.Validators.Post;
using FluentValidation;
using TodoListApp.WebApi.Models.Post;

public class TaskCommentPostModelValidator : AbstractValidator<TaskCommentPostModel>
{
    public TaskCommentPostModelValidator()
    {
        this.RuleFor(x => x.Comment)
            .NotEmpty().WithMessage("Comment is required.")
            .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.");

        this.RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("TaskId is required.")
            .NotEqual(Guid.Empty).WithMessage("TaskId must be a valid GUID.");
    }
}
