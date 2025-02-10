using TaskManager.Domain.Entities.Common;
using TaskManager.Domain.Enums;

namespace TaskManager.Domain.Entities;

public class User : BaseEntity<Guid>, IAuditEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public List<ApplicationRole> Roles { get; set; } = new();

    // A user can have many tasks
    public List<TaskItem> TaskItems { get; set; } = new();

    // Audit properties set by SavingChangesInterceptor
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}