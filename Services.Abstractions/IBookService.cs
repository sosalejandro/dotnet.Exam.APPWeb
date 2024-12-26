using Domain.Contracts;

namespace Services.Abstractions;

public interface IBookService 
{ 
    Task<IEnumerable<BookDto>> GetAllBooksAsync(CancellationToken stoppingToken = default);
    Task<BookDto> GetBookByIdAsync(Guid id, CancellationToken stoppingToken = default);
    Task<BookDto> CreateBookAsync(CreateBookDto bookDto, CancellationToken stoppingToken = default);
    Task<BookDto> UpdateBookAsync(Guid id, UpdateBookDto bookDto, CancellationToken stoppingToken = default);
    Task DeleteBookAsync(Guid id, CancellationToken stoppingToken = default);
}
