using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.TaskItem.DTOs;

public record TaskResponse(
    
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    TaskFrequency? Frequency,
    PriorityLevel PriorityLevel,
    bool IsCompleted,
    DateTime CreatedAt,
    DateTime UpdatedAt
    );