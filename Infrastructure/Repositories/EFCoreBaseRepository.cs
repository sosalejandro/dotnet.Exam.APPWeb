using Domain.Models;
using Domain.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

internal abstract class EFCoreBaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly BookDbContext _bookDbContext;

    protected EFCoreBaseRepository(BookDbContext hotelDbContext)
    {
        _bookDbContext = hotelDbContext
            ?? throw new ArgumentNullException(nameof(hotelDbContext));
    }

    public void Create(TEntity entity)
    {
        _bookDbContext.Set<TEntity>().Add(entity);
    }

    public void Delete(TEntity entity)
    {
        _bookDbContext.Set<TEntity>().Remove(entity);
    }

    public IQueryable<TEntity> FindAll(bool trackChanges)
    {
        return !trackChanges ?
            _bookDbContext.Set<TEntity>().AsNoTracking() :
            _bookDbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> expression, bool trackChanges)
    {
        return !trackChanges ?
            _bookDbContext.Set<TEntity>()
            .Where(expression)
            .AsNoTracking() :
            _bookDbContext.Set<TEntity>()
            .Where(expression);
    }

    public void Update(TEntity entity)
    {
        _bookDbContext.Set<TEntity>().Update(entity);
    }
}

