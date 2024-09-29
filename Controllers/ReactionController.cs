using BookStoreApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly IBookRepository Repo;
        public ReactionController(IBookRepository _Repo)
        {
            Repo = _Repo;
        }
        [Authorize]
        [HttpPost("like/{bookId:int}")]
        public async Task<IActionResult> Like(int bookId)
        {
            await Repo.Like(bookId);
            return Ok();
        }
        [Authorize]
        [HttpPost("Removelike/{bookId:int}")]
        public async Task<IActionResult> RemoveLike(int bookId)
        {
            await Repo.RemoveLike(bookId);
            return Ok();
        }
        [Authorize]
        [HttpPost("Dislike/{bookId:int}")]
        public async Task<IActionResult> DisLike(int bookId)
        {
            await Repo.Dislike(bookId);
            return Ok();
        }
        [Authorize]
        [HttpPost("RemoveDislike/{bookId:int}")]
        public async Task<IActionResult> RemoveDislike(int bookId)
        {
            await Repo.RemoveDislike(bookId);
            return Ok();
        }
    }
}
