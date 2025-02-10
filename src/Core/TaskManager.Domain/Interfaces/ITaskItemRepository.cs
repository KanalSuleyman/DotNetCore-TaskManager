using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces
{
    public interface ITaskItemRepository : IGenericRepository<TaskItem, Guid>
    {
        Task<List<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
