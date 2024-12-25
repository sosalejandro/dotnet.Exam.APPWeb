using APPWeb.Components;
using APPWeb.Extensions;
using BookPages.ViewModels;
using Domain.Contracts;
using dotnet.Exam.APPWeb.Domain.Repositories;
using dotnet.Exam.APPWeb.Infrastructure.Repositories;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.V1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure Entity Framework
builder.Services.AddDbContext<BookDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookDbContext"));
});

// Register application services
builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
builder.Services.AddScoped<IServiceManager, ServiceManager>();
builder.Services.AddScoped<BookState>();
builder.Services.AddScoped<BookViewModel>();
builder.Services.AddScoped<EditBookViewModel>();
builder.Services.AddScoped<IndexViewModel>();

var app = builder.Build();

// Configure middleware for production scenarios
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseRouting();
app.UseAntiforgery();

// Map Razor components and interactive components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode(); // Supports server-side rendering with interactivity


// Ensure database is created
app.EnsureDbIsCreated();

app.Run();
