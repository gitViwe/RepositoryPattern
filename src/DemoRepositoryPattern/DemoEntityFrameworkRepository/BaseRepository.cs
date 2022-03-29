using DemoEntityFrameworkRepository.Context;
using DemoEntityFrameworkRepository.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DemoEntityFrameworkRepository;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly RepositoryContext _context;

    public BaseRepository(RepositoryContext context)
    {
        _context = context;
    }

    public IQueryable<TEntity> FindAll()
    {
        return _context.Set<TEntity>().AsNoTracking();
    }

    public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression)
    {
        return _context.Set<TEntity>().Where(expression).AsNoTracking();
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
}
