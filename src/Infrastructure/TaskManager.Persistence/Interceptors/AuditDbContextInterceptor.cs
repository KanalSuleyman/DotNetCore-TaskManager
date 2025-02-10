using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TaskManager.Domain.Entities.Common;

namespace TaskManager.Persistence.Interceptors;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviours = new()
    {
        { EntityState.Added, AddBehaviour }, // Behavior for newly added entities.
        { EntityState.Modified, UpdateBehaviour } // Behavior for modified entities.
    };

    private static void AddBehaviour(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.CreatedAt = DateTime.Now;
        context.Entry(auditEntity).Property(a => a.UpdatedAt).IsModified = false;
    }

    private static void UpdateBehaviour(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.UpdatedAt = DateTime.Now;
        context.Entry(auditEntity).Property(a => a.CreatedAt).IsModified = false;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        // Iterates through all entities in the change tracker.
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
        {
            // Skips entities that do not implement the IAuditEntity interface.
            if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

            // Skips entities that are not in the Added or Modified state.
            if (entityEntry.State is not EntityState.Added or EntityState.Modified) continue;

            // Applies the appropriate audit behavior based on the entity's state.
            Behaviours[entityEntry.State](eventData.Context, auditEntity);
        }

        // Continues with the default saving changes process.
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}