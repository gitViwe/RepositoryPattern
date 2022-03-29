using DemoEntityFrameworkRepository.Domain;
using System.Linq.Expressions;

namespace DemoEntityFrameworkRepository;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    IQueryable<TEntity> FindAll();
    IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}
