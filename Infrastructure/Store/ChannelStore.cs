
using System.Threading.Channels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Infrastructure.Store;
public class ChannelStore<TEntity> where TEntity : BaseEntity
{
    private Channel<TEntity> _channel;

    public ChannelStore()
    {
        _channel = Channel.CreateUnbounded<TEntity>();
    }

    public async Task AddEntityAsync(TEntity entity)
    {
        await _channel.Writer.WriteAsync(entity);
    }

    public IAsyncEnumerable<TEntity> ReadAllAsync()
    {
        return _channel.Reader.ReadAllAsync();
    }

    public ValueTask<(bool Success, TEntity Entity)> TryReadAsync()
    {
        if (_channel.Reader.TryRead(out var entity))
        {
            return new ValueTask<(bool, TEntity)>((true, entity));
        }
        return new ValueTask<(bool, TEntity)>((false, null));
    }

    public void Complete()
    {
        _channel.Writer.Complete();
    }

    public void Reset()
    {
        _channel = Channel.CreateUnbounded<TEntity>();
    }
}

