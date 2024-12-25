using Domain.Contracts;
using dotnet.Exam.APPWeb.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Components;
using Services.Abstractions;

namespace BookPages.ViewModels;

public class EditBookViewModel
{
    private readonly IServiceManager _serviceManager;
    private readonly NavigationManager _navigationManager;
    private readonly BookState _bookState;

    public EditBookViewModel(IServiceManager serviceManager, NavigationManager navigationManager, BookState bookState)
    {
        _serviceManager = serviceManager;
        _navigationManager = navigationManager;
        _bookState = bookState;
    }

    public Book? Book { get; private set; }

    public async Task InitializeAsync(Guid id)
    {
        if (_bookState.CurrentBook != null && _bookState.CurrentBook.Id == id)
        {
            await Task.Run(() => Book = _bookState.CurrentBook.Adapt<Book>());
        }
        else
        {
            _navigationManager.NavigateTo("/books");
        }
    }

    public async Task SaveBookAsync()
    {
        if (Book != null)
        {
            var updateBook = Book.Adapt<UpdateBookDto>();
            try
            {
                await _serviceManager.BookService.UpdateBookAsync(Book.Id, updateBook);
                _navigationManager.NavigateTo("/books");
            }
            catch (Exception)
            {
                _navigationManager.NavigateTo("/books");
            }
        }
    }
}