using TaskManager.Domain.Enums;

namespace TaskManager.Application.Interfaces;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    string? Email { get; }
    List<ApplicationRole> Roles { get; }
    bool IsAuthenticated { get; }
}