using dotnet.Exam.APPWeb.Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;

namespace dotnet.Exam.APPWeb.Infrastructure.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IBookRepository> _lazyBookRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    public RepositoryManager(BookDbContext dbContext)
    {
        _lazyBookRepository = new(() =>
            new BookRepository(dbContext) ?? throw new ArgumentNullException(nameof(dbContext))
        );
        _lazyUnitOfWork = new(() =>
            new UnitOfWork(dbContext) ?? throw new ArgumentNullException(nameof(dbContext))
        );
    }
    public IBookRepository BookRepository => _lazyBookRepository.Value;

    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}