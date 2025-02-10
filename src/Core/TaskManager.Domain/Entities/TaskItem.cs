using TaskManager.Domain.Entities.Common;
using TaskManager.Domain.Enums;
using TaskManager.Domain.ValueObjects;

namespace TaskManager.Domain.Entities;

public class TaskItem : BaseEntity<Guid>, IAuditEntity
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DueDateRange? DueDateRange { get; set; }
    public TaskFrequency? Frequency { get; set; }
    public TaskPriority? TaskPriority { get; set; }
    public bool IsCompleted { get; set; } = false;

    // Audit properties set by SavingChangesInterceptor
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }


    // Foreign key
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;


}