using System;
using BookStoreApi.DataContext;
using BookStoreApi.Dto;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Services;

public class BookRepository : IBookRepository
{

    private readonly ApplicationDbContext Dbcontext;
    private readonly UserManager<User> UserM;
    public BookRepository(ApplicationDbContext _dbcontext, UserManager<User> _userManager)
    {
        Dbcontext = _dbcontext;
        UserM = _userManager;
    }




    public async Task<IEnumerable<Book>> GetAllBooks()
    {
        return await Dbcontext.Books.Select(p => p).AsNoTracking().ToListAsync();
    }

    public async Task<Book?> CreateBook(BookDTO book, string userId)
    {
        var item = new Book(Dbcontext){
            Title = book.Title,
            Description = book.Description,
            Author = book.Author,
            Price = book.Price,
            DateAdded = DateTime.Now,
            GenreId = book.GenreId,
            Genre = Dbcontext.Genre.Find(book.GenreId),
            UserId = userId,
        };

        item.User = await UserM.FindByEmailAsync(userId);

        Dbcontext.Books.Add(item);
        await SaveChanges();

        return item;
    }

    public async Task<bool> DeleteBook(int id, string userId)
    {
        var user = await  UserM.FindByEmailAsync(userId);
        var item = await Dbcontext.Books.FindAsync(id);

        if(item is null || item.User!.Id != user!.Id )
        {
            return false;
        }

        Dbcontext.Remove(item);

        return await SaveChanges();
        
    }


    public async Task<Book?> GetBooksById(int id)
    {
        Book? item = await Dbcontext.Books.FirstOrDefaultAsync(x => x.Id == id);
        
        if(item is null) return null; 
        
        return item;
    }

    public async Task<Book?> UpdateBook(int id, UpdateBookDTO book, string userId)
    {
        var user = await UserM.FindByEmailAsync(userId);
        
        if(user is null) return null;

        var item = Dbcontext.Books.Find(id);

        if(item is null) return null;

        var newItem = new Book(Dbcontext){
            Id = id,
            Title = book.Title,
            Description = book.Description,
            Author = book.Author,
            Price = book.Price,
            Comments = item.Comments,
            DateAdded = item.DateAdded,
            GenreId = book.GenreId,
            Genre = Dbcontext.Genre.Find(book.GenreId),
            UserId = user!.Id,
            User = user
        };

        await SaveChanges();


        return newItem;
    }
    
    public async Task<bool> SaveChanges()
    {
        try{
            await Dbcontext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> Like(int id)
    {
        var item = await Dbcontext.Books.FindAsync(id);
        if(item is null) return false;
        item.AddLike();

        Dbcontext.Books
        .Entry(item).CurrentValues.SetValues(item);

        return await SaveChanges();
    }

    public async Task<bool> RemoveLike(int id)
    {
        var item = await Dbcontext.Books.FindAsync(id);
        if(item is null) return false;
        item.UnLike();

        Dbcontext.Books
        .Entry(item).CurrentValues.SetValues(item);

        return await SaveChanges();
    }

    public async Task<bool> Dislike(int id)
    {
        var item = await Dbcontext.Books.FindAsync(id);
        if(item is null) return false;
        item.AddDislike();

        Dbcontext.Books
        .Entry(item).CurrentValues.SetValues(item);

        return await SaveChanges();
    }

    public async Task<bool> RemoveDislike(int id)
    {
        var item = await Dbcontext.Books.FindAsync(id);
        if(item is null) return false;
        item.UnDislike();

        Dbcontext.Books
        .Entry(item).CurrentValues.SetValues(item);

        return await SaveChanges();
    }
} 
