namespace TaskManager.Application.Features.TaskItem.DTOs;

public record TaskResponse(
    Guid Id,
    string Title,
    string Description,
    DateTime Deadline,
    Guid UserId);