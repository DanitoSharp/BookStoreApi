using System;
using BookStoreApi.DataContext;
using BookStoreApi.Dto;
using BookStoreApi.Models;

namespace BookStoreApi.Services;

public interface IBookRepository
{
    

    //get all
    Task<IEnumerable<Book>> GetAllBooks(); 
    //get by id
    Task<Book?> GetBooksById( int id); 
    //create
    Task<Book?> CreateBook(BookDTO book, string userId); 
    //update
    Task<Book?> UpdateBook(int id, UpdateBookDTO book, string userId);
    //delete
    Task<bool> DeleteBook(int id, string userId);

    Task<bool> Like(int id);
    Task<bool> RemoveLike(int id);
    Task<bool> Dislike(int id);
    Task<bool> RemoveDislike(int id);


    Task<string> UploadBookCover(int bookId, IFormFile file, HttpRequest request);

}
