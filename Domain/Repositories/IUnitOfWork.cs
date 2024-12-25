using Domain.Models;

namespace dotnet.Exam.APPWeb.Domain.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task AddTrackedEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;
}
