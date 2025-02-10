using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.TaskItem.Commands.UpdateTaskItem
{
    public sealed record UpdateTaskItemCommand : IRequest<ServiceResult>
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = default!;
        public string? Description { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public TaskFrequency? Frequency { get; init; }
        public PriorityLevel PriorityLevel { get; init; }
        public bool IsCompleted { get; init; } = false;
    }
}
