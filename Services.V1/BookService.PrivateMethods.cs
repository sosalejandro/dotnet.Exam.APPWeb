using Domain.Exceptions;
using dotnet.Exam.APPWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.V1;

internal sealed partial class BookService
{
    private async Task<Book> GetBookById(Guid id, bool trackChanges, CancellationToken stoppingToken = default)
    {
        try
        {
            Book book = await _repositoryManager
            .BookRepository
            .GetBookByIdAsync(id, trackChanges, stoppingToken);

            return book;
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
}