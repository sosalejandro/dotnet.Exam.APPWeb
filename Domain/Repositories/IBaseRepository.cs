using Domain.Models;
using System.Linq.Expressions;

namespace Domain.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public IQueryable<TEntity> FindAll(bool trackChanges);
    public IQueryable<TEntity> FindByCondition(
        Expression<Func<TEntity, bool>> expression,
        bool trackChanges);

    public void Create(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(TEntity entity);
}
