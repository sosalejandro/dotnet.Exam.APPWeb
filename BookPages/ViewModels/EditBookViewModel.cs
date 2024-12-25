using CommunityToolkit.Mvvm.ComponentModel;
using Domain.Contracts;
using dotnet.Exam.APPWeb.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Components;
using Services.Abstractions;

namespace BookPages.ViewModels;

public partial class EditBookViewModel : ObservableObject
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

    [ObservableProperty]
    private Book? _book;

    public event EventHandler<BookDto>? BookUpdated;

    public async Task InitializeAsync(Guid id)
    {
        if (_bookState.CurrentBook != null && _bookState.CurrentBook.Id == id)
        {
            Book = _bookState.CurrentBook.Adapt<Book>();
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
                var updatedBook = await _serviceManager.BookService.UpdateBookAsync(Book.Id, updateBook);
                BookUpdated?.Invoke(this, updatedBook);
                _navigationManager.NavigateTo("/books");
            }
            catch (Exception)
            {
                _navigationManager.NavigateTo("/books");
            }
        }
    }
}
