using Domain.Contracts;
using Domain.Exceptions;
using dotnet.Exam.APPWeb.Domain.Models;
using dotnet.Exam.APPWeb.Domain.Repositories;
using Mapster;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.V1;

internal sealed partial class BookService : IBookService
{
    private readonly IRepositoryManager _repositoryManager;

    public BookService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager
            ?? throw new ArgumentNullException(nameof(_repositoryManager));
    }
    public async Task<BookDto> CreateBookAsync(
        CreateBookDto bookDto,
        CancellationToken stoppingToken = default)
    {
        try
        {
            Book book = bookDto.Adapt<Book>();

            _repositoryManager
                .BookRepository
                .CreateBook(book);

            await _repositoryManager
                .UnitOfWork
                .AddTrackedEntityAsync(book);

            await _repositoryManager
                .UnitOfWork
                .SaveChangesAsync(stoppingToken);

            return book.Adapt<BookDto>();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the book", ex);
        }
    }

    public async Task DeleteBookAsync(Guid id, CancellationToken stoppingToken = default)
    {
        try
        {
            Book book = await GetBookById(id, false, stoppingToken);

            _repositoryManager
                .BookRepository
                .DeleteBook(book);

            await _repositoryManager
                .UnitOfWork
                .SaveChangesAsync(stoppingToken);
        }
        catch (BookNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the book", ex);
        }
    }

    public async Task<IEnumerable<BookDto>> GetAllBooksAsync( CancellationToken stoppingToken = default)
    {
        try
        {
            IEnumerable<Book> books = await _repositoryManager
                .BookRepository
                .GetAllBooksAsync(false, stoppingToken);

            return books.Adapt<IEnumerable<BookDto>>();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the books", ex);
        }
    }

    public async Task<BookDto> GetBookByIdAsync(Guid id, CancellationToken stoppingToken = default)
    {
        try
        {
            Book book = await GetBookById(id, false, stoppingToken);

            return book.Adapt<BookDto>();
        }
        catch (BookNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while getting the book", ex);
        }
    }

    public async Task<BookDto> UpdateBookAsync(Guid id, UpdateBookDto bookDto, CancellationToken stoppingToken = default)
    {
        try
        {
            var book = await GetBookById(id, false, stoppingToken);

            book.Name = bookDto.Name;
            book.Author = bookDto.Author;
            book.PublishedYear = bookDto.PublishedYear;

            _repositoryManager
                .BookRepository
                .UpdateBook(book);

            bookDto.Adapt<Book>();

            await _repositoryManager
                .UnitOfWork
                .SaveChangesAsync(stoppingToken);

            var result = new BookDto(
                id, 
                bookDto.Name, 
                bookDto.Author, 
                bookDto.PublishedYear, 
                bookDto.Version);

            return result;
        }
        catch (BookNotFoundException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the book", ex);
        }
    }
}