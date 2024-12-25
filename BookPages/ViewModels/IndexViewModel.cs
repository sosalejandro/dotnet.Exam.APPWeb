using Domain.Contracts;
using Microsoft.AspNetCore.Components;
using Services.Abstractions;

namespace BookPages.ViewModels;

public class IndexViewModel
{
    private readonly IServiceManager _serviceManager;
    private readonly NavigationManager _navigationManager;
    private readonly BookState _bookState;

    public IndexViewModel(IServiceManager serviceManager, NavigationManager navigationManager, BookState bookState)
    {
        _serviceManager = serviceManager;
        _navigationManager = navigationManager;
        _bookState = bookState;
    }

    public List<BookDto> Books { get; private set; } = new();
    public bool ShowErrorModal { get; private set; } = false;
    public string ErrorMessage { get; private set; } = string.Empty;

    public async Task InitializeAsync()
    {
        await LoadBooksAsync();
    }

    public async Task LoadBooksAsync()
    {
        try
        {
            Books = (await _serviceManager.BookService.GetAllBooksAsync()).ToList();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    public async Task AddBookAsync()
    {
        try
        {
            var newBook = new CreateBookDto("New Book", "Someone", 2023, 1);
            await _serviceManager.BookService.CreateBookAsync(newBook);
            await LoadBooksAsync();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    public void EditBook(BookDto book)
    {
        _bookState.CurrentBook = book;
        _navigationManager.NavigateTo($"/books/edit/{book.Id}");
    }

    public void ViewBook(BookDto book)
    {
        _bookState.CurrentBook = book;
        _navigationManager.NavigateTo($"/books/{book.Id}");
    }

    public async Task DeleteBookAsync(Guid id)
    {
        try
        {
            await _serviceManager.BookService.DeleteBookAsync(id);
            await LoadBooksAsync();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void ShowError(string message)
    {
        ErrorMessage = message;
        ShowErrorModal = true;
    }

    public void CloseErrorModal()
    {
        ShowErrorModal = false;
    }
}
