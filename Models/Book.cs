using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using BookStoreApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Models;

public class Book
{
    [NotMapped]
    private readonly ApplicationDbContext Dbcontext;
    public Book(ApplicationDbContext _Db)
    {
        Dbcontext = _Db ?? throw new ArgumentNullException(nameof(_Db));
    }



    public int Id { get; set; }
    public string? BookCover { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Author { get; set; }
    public decimal Price { get; set; }
    
    public IEnumerable<Comments>? Comments { get{
        return Dbcontext.Comments.Where(c => c.BookId == this.Id)
        .Select(c => c).ToList();
    } set{
        Comments = value;
    } }


    public int Likes { get{
        return Likes;
    } set{
        Likes = value <= 0 ? 0 : value;
    } }
    
    public int DisLikes { get{
        return DisLikes;
    } set{
        DisLikes = value <= 0 ? 0 : value;
    } }

    public string? UserId { get; set; }
    public User? User { get; set; }

    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    public DateTime DateAdded { get; set; }

    public void AddLike()
    {
        Likes++;
    }
    public void AddDislike()
    {
        DisLikes++;
    }
    public void UnLike()
    {
        Likes--;
    }
    public void UnDislike()
    {
        DisLikes--;
    }

    
}
