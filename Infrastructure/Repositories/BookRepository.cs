using Domain.Exceptions;
using dotnet.Exam.APPWeb.Domain.Models;
using dotnet.Exam.APPWeb.Domain.Repositories;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Exam.APPWeb.Infrastructure.Repositories;

internal sealed class BookRepository : EFCoreBaseRepository<Book>, IBookRepository
{
    private readonly BookDbContext _context;
    public BookRepository(BookDbContext context) : base(context)
    {
        _context = context;
    }

    public void CreateBook(Book book)
    {
        Create(book);
    }

    public void DeleteBook(Book book)
    {
        Delete(book);
    }

    public async Task<IEnumerable<Book>> GetAllBooksAsync(bool trackChanges, CancellationToken stoppingToken = default)
    {
        return await FindAll(trackChanges)
            .ToListAsync(stoppingToken);
    }

    public async Task<Book> GetBookByIdAsync(Guid bookId, bool trackChanges, CancellationToken stoppingToken = default)
    {
        var Book = await FindByCondition(b => b.Id.Equals(bookId), trackChanges)
            .SingleOrDefaultAsync(stoppingToken);

        // If the book is not found, throw a BookNotFoundException
        // Using coalesce operator to throw an exception if the book is null
        return Book ?? throw new BookNotFoundException(bookId);
    }

    public async Task<IEnumerable<Book>> GetBooksByIdsAsync(IEnumerable<Guid> ids, bool trackChanges, CancellationToken stoppingToken = default)
    {
        var Books = await FindByCondition(b => ids.Contains(b.Id), trackChanges)
            .ToListAsync(stoppingToken);

        return Books ?? [];
    }

    public void UpdateBook(Book book)
    {
        Update(book);
    }

}
