using MediatR;
using TaskManager.Application.Features.TaskItem.DTOs;

namespace TaskManager.Application.Features.TaskItem.Queries.GetTaskItem;

public class GetTaskItemQuery(Guid id) : IRequest<ServiceResult<TaskItemDto>>
{
    public Guid Id { get; set; } = id;
}