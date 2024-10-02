using System.Linq.Expressions;

namespace Jobcandidate.Shared;

public interface IRepositoryBase<T> where T : class
{
    Task<T> Create(T entity);
    Task<IEnumerable<T>> CreateRange(IEnumerable<T> entities);
    Task<T> Delete(T entity);
    IQueryable<T> FindAll(bool trackChanges);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = true);
    Task<T> Update(T entity);
}
