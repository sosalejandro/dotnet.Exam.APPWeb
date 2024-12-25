using Infrastructure;

namespace APPWeb.Extensions;

public static class WebApplicationExtension
{
    public static void EnsureDbIsCreated(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        BookDbContext context = scope.ServiceProvider.GetService<BookDbContext>();
        context.Database.EnsureCreated();
        context.Dispose();
    }
}
