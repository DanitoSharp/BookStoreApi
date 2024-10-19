using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using BookStoreApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Models;

public class Book
{
    // [NotMapped]
    // private readonly ApplicationDbContext Dbcontext;
    // public Book(ApplicationDbContext _Db)
    // {
    //     Dbcontext = _Db ?? throw new ArgumentNullException(nameof(_Db));
    // }



    public int Id { get; set; }
    public string? BookCover { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Author { get; set; }
    public decimal Price { get; set; }
    
    public List<Comments>? Comments{get; set;}

    private int likes {get; set;} = 0;
    public int Likes 
    {
        get{ return likes; }
        set{ likes = likes <= 0 ? 0 : value ; }
    }
    
    
    private int dislikes { get; set;} = 0;
    public int DisLikes
    { 
        get{ return dislikes; }
        set{ dislikes = dislikes <= 0 ? 0 : value;}
    }

    public string? UserId { get; set; }
    public User? User { get; set; }

    public int GenreId { get; set; }
    public Genre? Genre { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.Now;

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
