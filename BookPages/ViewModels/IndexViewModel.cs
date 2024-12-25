using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using Domain.Contracts;
using Microsoft.AspNetCore.Components;
using Services.Abstractions;

namespace BookPages.ViewModels;

public partial class IndexViewModel : ObservableObject
{
    private readonly IServiceManager _serviceManager;
    private readonly NavigationManager _navigationManager;
    private readonly BookState _bookState;
    private readonly EditBookViewModel _editBookViewModel;

    public IndexViewModel(IServiceManager serviceManager, NavigationManager navigationManager, BookState bookState, EditBookViewModel editBookViewModel)
    {
        _serviceManager = serviceManager;
        _navigationManager = navigationManager;
        _bookState = bookState;
        _editBookViewModel = editBookViewModel;
        _editBookViewModel.BookUpdated += OnBookUpdated;
        Books = new ObservableCollection<BookDto>();
    }

    [ObservableProperty]
    private ObservableCollection<BookDto> _books;

    [ObservableProperty]
    private bool _showErrorModal;

    [ObservableProperty]
    private string _errorMessage;

    public async Task InitializeAsync()
    {
        await LoadBooksAsync();
    }

    public async Task LoadBooksAsync()
    {
        try
        {
            var books = await _serviceManager.BookService.GetAllBooksAsync();
            Books = new ObservableCollection<BookDto>(books);
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
            var createdBook = await _serviceManager.BookService.CreateBookAsync(newBook);
            Books.Add(createdBook);
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
            var bookToRemove = Books.FirstOrDefault(b => b.Id == id);
            if (bookToRemove != null)
            {
                Books.Remove(bookToRemove);
            }
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void OnBookUpdated(object? sender, BookDto updatedBook)
    {
        var bookToUpdate = Books.FirstOrDefault(b => b.Id == updatedBook.Id);
        if (bookToUpdate != null)
        {
            var index = Books.IndexOf(bookToUpdate);
            Books[index] = updatedBook;
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
