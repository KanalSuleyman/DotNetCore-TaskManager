using AutoMapper;
using TaskManager.Application.Features.TaskItem.DTOs;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Features.TaskItem.Queries.GetTasksByUser;

public class GetTasksByUserQueryMappingProfile : Profile
{
    public GetTasksByUserQueryMappingProfile()
    {
        CreateMap<Domain.Entities.TaskItem, TaskResponse>()
            .ConstructUsing(src => new TaskResponse(
                src.Title,
                src.Description ?? string.Empty,  // Handle nullability for Description
                src.DueDateRange != null ? src.DueDateRange.StartDate : DateTime.MinValue,  // Handle nullability for StartDate
                src.DueDateRange != null ? src.DueDateRange.EndDate : DateTime.MinValue,    // Handle nullability for EndDate
                src.Frequency,
                src.TaskPriority != null ? (PriorityLevel)src.TaskPriority.PriorityLevel : PriorityLevel.Medium,  // Handle PriorityLevel conversion
                src.IsCompleted,
                src.CreatedAt,
                src.UpdatedAt
            ));
    }
}