using System;
using BookStoreApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Services;

public static class MigrationsExtention
{

    public static void AutoMigrations(this WebApplication app)
    {
       using var scope = app.Services.CreateScope();
       var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); 
       dbContext.Database.Migrate();
    }

}
