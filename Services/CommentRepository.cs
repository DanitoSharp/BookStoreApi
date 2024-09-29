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
    public async Task<Comments> Create(CommentDTO comment, string userId)
    {
        var user = await Manager.FindByEmailAsync(userId);
        var item = new Comments(){
            Body = comment.Body,
            BookId = comment.BookId,
            Book = Dbcontext.Books.Find(comment.BookId),
            UserId = user!.Id,
            User = user
        };

        Dbcontext.Comments.Add(item);
        
        await SaveChanges();


        return item;
    }

    public async Task<bool> Delete(int id, string userId)
    {
        await Dbcontext.Comments.Where( c => c.Id == id && c.UserId == userId)
        .Select(c => c)
        .ExecuteDeleteAsync();

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
}