using Domain.Models;
using dotnet.Exam.APPWeb.Domain.Repositories;
using Infrastructure.Store;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly BookDbContext _dbContext;
    private readonly ChannelStore<BaseEntity> _channelStore;
    public UnitOfWork(BookDbContext dbContext)
    {
        _dbContext = dbContext;
        _channelStore = new ChannelStore<BaseEntity>();
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        // Detach entities from the context after saving
        while (true)
        {
            var (success, entity) = await _channelStore.TryReadAsync();
            if (!success)
            {
                break;
            }
            _dbContext.Entry(entity).State = EntityState.Detached;
        }

        // Complete the channel to clean up
        _channelStore.Complete();

        // Reopen the channel by creating a new instance
        _channelStore.Reset();

        return result;
    }

    public async Task AddTrackedEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        await _channelStore.AddEntityAsync(entity);
    }
}