using System.Linq.Expressions;

namespace TaskManager.Domain.Interfaces;

public interface IGenericRepository<T, in TId> where T : class where TId : struct
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(TId id, CancellationToken cancellationToken = default);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    IQueryable<T> Where(Expression<Func<T, bool>> predicate);

    ValueTask<T?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    ValueTask AddAsync(T entity, CancellationToken cancellationToken = default);

    void Update(T entity);

    void Delete(T entity);
}