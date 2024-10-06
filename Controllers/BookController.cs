using System.Security.Claims;
using BookStoreApi.Dto;
using BookStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository Repo;
        public BookController(IBookRepository _Repo)
        {
            Repo = _Repo;
        }
        [HttpGet("GetAllBook")]
        public async Task<IActionResult> AllBooks()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return BadRequest("User does not exist!");

            var items = await Repo.GetAllBooks();

            return Ok(items);
        }
        [HttpGet("GetBookById/{id:int}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return BadRequest("User does not exist!");

            var items = await Repo.GetBooksById(id);

            return Ok(items);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDTO book)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return BadRequest("User does not exist!");

            var item = await Repo.CreateBook(book, userId);

            return CreatedAtAction(nameof(CreateBook), new { Id = item!.Id }, item);
        }

        [Authorize]
        [HttpPost("Update/{id:int}")]
        public async Task<IActionResult> UpdateBook(int id, UpdateBookDTO book)
        {

            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return BadRequest("User does not exist!");

            var item = await Repo.UpdateBook(id, book, userId);

            return Ok(item);

        }

        [Authorize]
        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null) return BadRequest("User does not exist!");

            var isDeleted = await Repo.DeleteBook(id, userId);

            if (isDeleted)
                return NoContent();

            return NotFound();

        }

        [HttpPost("{id:int}/upload-book-cover")]
        public async Task<IActionResult> UploadBookCover([FromQuery]int id, [FromForm] IFormFile file)
        {
            
            var fileUrl = await Repo.UploadBookCover(id, file, Request);

            if (fileUrl == "No file was uploaded.") return BadRequest("No file was uploaded.");
            if (fileUrl == "Only JPG and PNG image formats are supported.") return BadRequest("Only JPG and PNG image formats are supported.");
            if (fileUrl == "Bad Request") return BadRequest();

            return Ok(new { FilePath = fileUrl });
        }



    }
}
