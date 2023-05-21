using CleanArchitecture.ApplicationCore.Abstractions.Repositories;
using CleanArchitecture.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    internal DbSet<T> dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        this.dbSet = _context.Set<T>();
    }

    public async Task CreateAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter, bool tracked = false)
    {
        if (filter is null) throw new NullReferenceException("filtro non può essere null");
        IQueryable<T> query = this.dbSet;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        return await query.AnyAsync(filter);
    }   
    // x => x.Id == filter
    public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = false)
    {
        IQueryable<T> query = this.dbSet;
        if(!tracked)
        {
            query = query.AsNoTracking();
        }
        if( filter != null)
        {
            query = query.Where(filter);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool tracked = false)
    {
        if (filter is null) throw new NullReferenceException("filtro non può essere null");
        IQueryable <T> query = this.dbSet;

        if(!tracked)
        {
            query = query.AsNoTracking();
        }
        
        return await query.SingleAsync(filter);
        
    }

    public virtual void Remove(T entity)
    {
        if (entity is null) throw new NullReferenceException("Oggetto non trovato");
        dbSet.Remove(entity);
    }
}
