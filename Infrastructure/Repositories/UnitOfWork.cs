using dotnet.Exam.APPWeb.Domain.Repositories;

namespace Infrastructure.Repositories;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly BookDbContext _dbContext;
    public UnitOfWork(BookDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
       return _dbContext.SaveChangesAsync(cancellationToken);
    }
}