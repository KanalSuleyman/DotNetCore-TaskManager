using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace TaskManager.Application.Features.TaskItem.Commands.UpdateTaskItem
{
    public class UpdateTaskItemCommandValidator : AbstractValidator<UpdateTaskItemCommand>
    {
        public UpdateTaskItemCommandValidator()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(200).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(300).WithMessage("{PropertyName} must not exceed 200 characters.");
            RuleFor(t => t.StartDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThanOrEqualTo(DateTime.UtcNow).WithMessage("{PropertyName} must be greater than today.");
            RuleFor(t => t.EndDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThanOrEqualTo(p => p.StartDate).WithMessage("{PropertyName} must be greater than Start Date.");
            RuleFor(t => t.Frequency)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(t => t.PriorityLevel)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
    }
}
