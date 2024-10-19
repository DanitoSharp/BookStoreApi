using System;

namespace BookStoreApi.Models;

public class Comments
{

    public int Id { get; set; }
    public required string Body { get; set; }

    public int Likes { get; set; } = 0;

    public int BookId { get; set; }
    public Book? Book { get; set; }

    public required string? UserId { get; set; }
    public User? User { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.Now;

    public void AddLikes()
    {
        Likes++;
    }
    public void UnLikes()
    {
        Likes--;
    }

}
