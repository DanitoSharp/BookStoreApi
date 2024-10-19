using System;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.DataContext;

public class ApplicationDbContext : IdentityDbContext<User>
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Comments> Comments => Set<Comments>();
    public DbSet<Genre> Genre => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Book>().HasOne(t => t.User) // Navigation property
        //     .WithMany()                               // One user can have many todo items
        //     .HasForeignKey(t => t.UserId)             // Foreign key
        //     .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder.Entity<Book>().HasOne(t => t.Genre) // Navigation property
        //     .WithMany()                               // One user can have many todo items
        //     .HasForeignKey(t => t.GenreId)             // Foreign key
        //     .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder.Entity<Comments>().HasOne(t => t.User) // Navigation property
        //     .WithMany()                               // One user can have many todo items
        //     .HasForeignKey(t => t.UserId)             // Foreign key
        //     .OnDelete(DeleteBehavior.Cascade);

        // modelBuilder.Entity<Comments>().HasOne(t => t.Book) // Navigation property
        //     .WithMany()                                 // One user can have many todo items
        //     .HasForeignKey(t => t.BookId)               // Foreign key
        //     .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Genre>().HasData(
            new Genre()
            {
                Id = 1,
                Name = "Autobiography"
            },
            new Genre()
            {
                Id = 2,
                Name = "Biography"
            },
            new Genre()
            {
                Id = 3,
                Name = "Business"
            },
            new Genre()
            {
                Id = 4,
                Name = "Children's"
            },
            new Genre()
            {
                Id = 5,
                Name = "Contemporary"
            },
            new Genre()
            {
                Id = 6,
                Name = "Cookbook"
            },
            new Genre()
            {
                Id = 7,
                Name = "Crime Fiction"
            },
            new Genre()
            {
                Id = 8,
                Name = "Dystopian"
            },
            new Genre()
            {
                Id = 9,
                Name = "Fantasy"
            },
            new Genre()
            {
                Id = 10,
                Name = "Health and Wellness"
            },
            new Genre()
            {
                Id = 11,
                Name = "Historical Fiction"
            },
            new Genre()
            {
                Id = 12,
                Name = "Horror"
            },
            new Genre()
            {
                Id = 13,
                Name = "Literary Fiction"
            },
            new Genre()
            {
                Id = 14,
                Name = "Memoir"
            },
            new Genre()
            {
                Id = 15,
                Name = "Mystery"
            },
            new Genre()
            {
                Id = 16,
                Name = "Romance"
            },
            new Genre()
            {
                Id = 17,
                Name = "Science"
            },
            new Genre()
            {
                Id = 18,
                Name = "Science Fiction"
            },
            new Genre()
            {
                Id = 19,
                Name = "Self-Help"
            },
            new Genre()
            {
                Id = 20,
                Name = "Thriller"
            },
            new Genre()
            {
                Id = 21,
                Name = "Travel"
            },
            new Genre()
            {
                Id = 22,
                Name = "Young Adult"
            }
        );
    }

}
