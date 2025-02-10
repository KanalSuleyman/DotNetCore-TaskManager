using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Interfaces;
using TaskManager.Persistence.Context;

namespace TaskManager.Persistence.Repositories
{
    public class TaskItemRepository(TaskManagerDbContext context) : GenericRepository<TaskItem, Guid>(context), ITaskItemRepository
    {
        public async Task<List<TaskItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await context.TaskItems.Where(t => t.UserId == userId).ToListAsync(cancellationToken);
        }
    }
}
