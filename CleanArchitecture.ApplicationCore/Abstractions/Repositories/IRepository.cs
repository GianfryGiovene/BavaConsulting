using System.Linq.Expressions;

namespace CleanArchitecture.ApplicationCore.Abstractions.Repositories;

public interface IRepository<T>
{
    public Task CreateAsync(T entity);

    public Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracked = false);

    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false);

    public Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool tracked = false);

    public void Remove(T entity);
}
