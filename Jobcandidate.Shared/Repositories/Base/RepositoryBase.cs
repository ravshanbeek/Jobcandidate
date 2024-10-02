using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Jobcandidate.Shared;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected readonly JobCandidateContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(JobCandidateContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T> Create(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> CreateRange(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
        return entities;
    }

    public async Task<T> Delete(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return trackChanges ? _dbSet : _dbSet.AsNoTracking();
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = true)
    {
        return trackChanges ? _dbSet.Where(expression) : _dbSet.Where(expression).AsNoTracking();
    }

    public async Task<T> Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}
