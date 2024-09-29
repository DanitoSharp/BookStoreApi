using System;
using Microsoft.AspNetCore.Identity;

namespace BookStoreApi.Models;

public class User : IdentityUser
{
    public string FullName
    {
        get
        { 
            return FirstName + " " + LastName;
        }
    }
    public required string FirstName { get; set;}
    public required string LastName { get; set;}
    public required DateOnly DateOfBirth { get; set;}
    public List<Book> Cart {get; set;} = new List<Book>();



}
