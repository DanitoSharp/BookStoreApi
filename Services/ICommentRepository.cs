using System;
using System.Collections.Generic;
using BookStoreApi.Dto;
using BookStoreApi.Models;

namespace BookStoreApi.Services;

public interface ICommentRepository
{
    Task<IEnumerable<Comments>> AllComments();
    Task<Comments?> Create(CommentDTO comment, string userId);
    Task<bool> Update(int commentId, CommentUpdateDTO comment, string userId);
    Task<bool?> Delete(int id, string userId);
    Task<IEnumerable<Comments>> GetComments(int BookId);
    Task<bool> SaveChanges();
    Task<Comments?> GetSingle(int id);
}
