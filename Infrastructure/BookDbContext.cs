using dotnet.Exam.APPWeb.Domain.Models;
using Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options): base(options)
    {
            
    }
    public DbSet<Book> Books { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
    }
}
