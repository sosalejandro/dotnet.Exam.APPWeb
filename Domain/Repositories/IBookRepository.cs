using Domain.Repositories;
using dotnet.Exam.APPWeb.Domain.Models;

namespace dotnet.Exam.APPWeb.Domain.Repositories;

public interface IBookRepository : IBaseRepository<Book>
{
    void CreateBook(Book book);
    void DeleteBook(Book book);
    Task<IEnumerable<Book>> GetAllBooksAsync(
        bool trackChanges,
        CancellationToken stoppingToken = default);

    Task<Book> GetBookByIdAsync(
        Guid bookId,
        bool trackChanges,
        CancellationToken stoppingToken = default);
    Task<IEnumerable<Book>> GetBooksByIdsAsync(
        IEnumerable<Guid> ids,
        bool trackChanges,
        CancellationToken stoppingToken = default);
    void UpdateBook(Book book);

}
