using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Persistence.Context;
using TaskManager.Persistence.Repositories;

namespace TaskManager.Persistence;

/// <summary>
/// Implements the unit of work pattern using EF Core.
/// </summary>
public class UnitOfWork(TaskManagerDbContext context, ITaskItemRepository? taskItemRepository, IUserRepository userRepository) : IUnitOfWork
{
    private ITaskItemRepository? _taskItemRepository = taskItemRepository;
    private IUserRepository? _userRepository = userRepository;

    /// <summary>
    /// Gets the TaskItem repository.
    /// </summary>
    public ITaskItemRepository TaskItemRepository =>
        _taskItemRepository ??= new TaskItemRepository(context);
    public IUserRepository UserRepository =>
        _userRepository ??= new UserRepository(context);

    /// <summary>
    /// Commits all pending changes to the database asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        // Internally calls EF Core's SaveChangesAsync.
        return await context.SaveChangesAsync(cancellationToken);
    }
}