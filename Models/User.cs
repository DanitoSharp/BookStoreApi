using System;
using Microsoft.AspNetCore.Identity;

namespace BookStoreApi.Models;

public class User : IdentityUser
{
    public required string FirstName { get; set;}
    public required string LastName { get; set;}
    public string FullName {get{ return $"{FirstName} {LastName}";}}
    public required DateOnly DateOfBirth { get; set;}
    public List<Book>? Cart {get; set;} = new List<Book>();

}
// {
//   "title": "Daniels book",
//   "description": "This is the first book here",
//   "author": "Hello Writer",
//   "price": 30,
//   "genreId": 3
// }