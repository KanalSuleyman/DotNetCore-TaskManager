using MediatR;
using TaskManager.Domain.Enums;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Application.Features.TaskItem.Commands.CreateTaskItem
{
    public record CreateTaskItemCommand(
        string Title,
        string? Description,
        DateTime StartDate,
        DateTime EndDate,
        TaskFrequency? Frequency,
        PriorityLevel PriorityLevel,
        bool IsCompleted = false
    ) : IRequest<ServiceResult<Guid>>
    {
        public Guid UserId { get; set; }
    }
        
}
