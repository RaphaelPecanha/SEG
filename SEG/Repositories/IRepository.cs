using System.Linq.Expressions;

namespace SEG.Repositories;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetUnicoAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> GetLstAsync(Expression<Func<T, bool>> predicate);
    Task<bool> GetUnicoBoolAsync(Expression<Func<T, bool>> predicate);
    T Create(T entity);
    T Update(T entity);
    T Delete(T entity);

}
