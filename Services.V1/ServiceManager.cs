using dotnet.Exam.APPWeb.Domain.Repositories;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.V1;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IBookService> _lazyBookService;
    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _lazyBookService = new(
            () => new BookService(repositoryManager) ?? throw new ArgumentNullException(nameof(repositoryManager))
            );
    }
    public IBookService BookService => _lazyBookService.Value;
}
