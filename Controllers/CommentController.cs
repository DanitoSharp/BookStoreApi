using System.Security.Claims;
using BookStoreApi.Dto;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository CommentRepo;
        public CommentController(ICommentRepository _CommentRepo)
        {
            CommentRepo = _CommentRepo;
        }

        [HttpGet("AllComments")]
        public async Task<IActionResult> AllComment()
        {
            var comments = await CommentRepo.AllComments();
            return Ok(comments);
        }


        //Create
        [Authorize]
        [HttpPost("WriteComment")]
        public async Task<IActionResult> Create(CommentDTO comment)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return Unauthorized();

            var item = await CommentRepo.Create(comment, userId);

            if(item is null)
            {
                return Unauthorized("Try again later.");
            }
            return CreatedAtAction(nameof(GetSingleComment), new{ Id = item.Id}, item);//CreatedAtAction(nameof(Create));
        }
        
        
        [HttpGet("GetBookComments/{bookId:int}")]
        public async Task<IActionResult> BookComments(int bookId)
        {
            var items = await CommentRepo.GetComments(bookId);

            return Ok(items);
        }


        [HttpGet("GetComment/{id:int}")]
        public async Task<IActionResult> GetSingleComment(int id)
        {
            var items = await CommentRepo.GetSingle(id);

            return Ok(items);
        }


        //Delete
        [Authorize]
        [HttpDelete("DeleteComment/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return Unauthorized();

            var delete = await CommentRepo.Delete(id, userId);
            if(delete is false)
            return NotFound();

            return NoContent();
        }
        //Update
        [Authorize]
        [HttpPost("UpdateComment/{commentId:int}")]
        public async Task<IActionResult> Update(int commentId, CommentUpdateDTO comment)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return Unauthorized();

            var updateItem = await CommentRepo.Update(commentId, comment, userId);

            if (updateItem is false) return BadRequest("Comment not found");

            return NoContent();
        }
    }
}
