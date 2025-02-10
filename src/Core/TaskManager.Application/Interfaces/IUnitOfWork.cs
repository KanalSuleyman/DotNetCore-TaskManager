using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for the unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the TaskItem repository.
        /// </summary>
        ITaskItemRepository TaskItemRepository { get; }
        IUserRepository UserRepository { get; }

        /// <summary>
        /// Commits all pending changes to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The number of state entries written to the database.</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}
