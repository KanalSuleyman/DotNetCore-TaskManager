using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManager.Domain.Entities.Common;
using TaskManager.Domain.Interfaces;
using TaskManager.Persistence.Context;

namespace TaskManager.Persistence.Repositories;

public class GenericRepository<T, TId>(TaskManagerDbContext context) : IGenericRepository<T, TId> where T : BaseEntity<TId> where TId : struct
{
    protected TaskManagerDbContext Context = context;

    private readonly DbSet<T> _dbSet = context.Set<T>();


    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default) => await _dbSet.ToListAsync(cancellationToken);
        

    public async Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        => await _dbSet.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync(cancellationToken);
        

    public async Task<bool> AnyAsync(TId id, CancellationToken cancellationToken = default) => await _dbSet.AnyAsync(x => x.Id.Equals(id), cancellationToken);

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) => await _dbSet.AnyAsync(predicate, cancellationToken);

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsNoTracking();

    public async ValueTask<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default) => await _dbSet.FindAsync(new object[] { id }, cancellationToken);

    public async ValueTask AddAsync(T entity, CancellationToken cancellationToken = default) => await _dbSet.AddAsync(entity, cancellationToken);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);
}