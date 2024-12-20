using System;
using System.Collections.Generic;
using BookStoreApi.DataContext;
using BookStoreApi.Dto;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Services;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext Dbcontext;
    private readonly UserManager<User> Manager;
    public CommentRepository(ApplicationDbContext _DbContext, UserManager<User> _Manager)
    {
        Dbcontext = _DbContext;
        Manager = _Manager;
    }
    
    public async Task<Comments?> Create(CommentDTO comment, string userId)
    {
        var user = await Manager.FindByEmailAsync(userId);
        if (user == null)
        {
            return null;
        }
        var item = new Comments(){
            Body = comment.Body,
            BookId = comment.BookId,
            Book = Dbcontext.Books.Find(comment.BookId),
            UserId = user!.Id,
            User = user
        };

        var book = Dbcontext.Books.Find(comment.BookId);
        if(book is null)
        {
            return null;
        }

        Dbcontext.Comments.Add(item);

        book.Comments.Add(item);

        Dbcontext.Books.Update(book);
        
        await SaveChanges();


        return item;
    }

    public async Task<bool?> Delete(int id, string userId)
    {
        var comment = Dbcontext.Comments.Find(id); // gets the comment
        if (comment is null)
        {
            return null;
        }
        var book = Dbcontext.Books.Find(comment.BookId); //gets the book that's related to the comment
        
        // if the book attached to the comment does not exist, delete the comment.
        if (book is null)
        {
            Dbcontext.Comments.Where( c => c.Id == id).ExecuteDelete();

            return await SaveChanges();
        }
        
        book.Comments.RemoveAll(c => c.Id == id); //removes the comment in list

        Dbcontext.Books.Update(book);

        await Dbcontext.Comments.Where( c => c.Id == id && c.UserId == userId)
        .ExecuteDeleteAsync(); //deletes the commemt completly from the database

        return await SaveChanges();
    }

    public async Task<IEnumerable<Comments>> GetComments(int bookId)
    {
        return await Dbcontext.Comments.Where(c => c.BookId == bookId).Select(c => c)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task<bool> Update(int commentId, CommentUpdateDTO comment, string userId)
    {
        var user = await Manager.FindByEmailAsync(userId);

        var item = Dbcontext.Comments.Find(commentId);

        if(item!.UserId != user!.Id)
        {
            return false;
        }

        var updateComment = new Comments(){
            Id = commentId,
            Body = comment.Body,
            BookId = comment.BookId,
            UserId = user!.Id,
            User = user
        };

        Dbcontext.Comments.Entry(item).CurrentValues.SetValues(updateComment);

        var book = Dbcontext.Books.Find(item.BookId);
        var commentInList = book!.Comments!.FirstOrDefault(x => x.Id == commentId && userId == x.UserId);
        
        commentInList!.Id = commentId;
        commentInList.Body = comment.Body;
        commentInList.BookId = comment.BookId;
        commentInList.UserId = user!.Id;
        commentInList.User = user;


        await SaveChanges();

        return true;
    }

    public async Task<Comments?> GetSingle(int id)
    {
        var item = await Dbcontext.Comments.FindAsync(id);
        
        if(item is null) return null;
        
        return item;
    }
    public async Task<bool> SaveChanges()
    {
        await Dbcontext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Comments>> AllComments()
    {
        return await Dbcontext.Comments.Include( x => x.Book).Select( x => x).AsNoTracking().ToListAsync();
    }
}
