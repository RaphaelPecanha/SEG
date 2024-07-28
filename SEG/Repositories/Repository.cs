using Microsoft.EntityFrameworkCore;
using SEG.Context;
using System.Linq.Expressions;

namespace SEG.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetUnicoAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<T>> GetLstAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<bool> GetUnicoBoolAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().AnyAsync(predicate);
    }

    public T Create(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        _context.Set<T>().Add(entity);
        //_context.SaveChanges();
        return entity;
    }

    public T Delete(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        _context.Set<T>().Remove(entity);
        //_context.SaveChanges();

        return entity;
    }

    public T Update(T entity)
    {
        if (entity is null) throw new ArgumentNullException(nameof(entity));

        _context.Set<T>().Update(entity);
        //_context.SaveChanges();
        return entity;
    }


}
