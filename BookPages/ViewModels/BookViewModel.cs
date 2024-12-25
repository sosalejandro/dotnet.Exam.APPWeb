using Domain.Contracts;
using dotnet.Exam.APPWeb.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPages.ViewModels;

public class BookViewModel
{
    private readonly NavigationManager _navigationManager;
    private readonly BookState _bookState;

    public BookViewModel(NavigationManager navigationManager, BookState bookState)
    {
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

    public void EditBook(Guid id)
    {
        _navigationManager.NavigateTo($"/books/edit/{id}");
    }
}