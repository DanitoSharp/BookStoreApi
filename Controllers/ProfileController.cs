using System.Security.Claims;
using BookStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BookStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<User> UserM;
        public ProfileController(UserManager<User> _userM)
        {
            UserM = _userM;
        }

        [HttpGet("UserDetails")]
        public async Task<IActionResult> UserDetails()
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null) return BadRequest();

            var item = await UserM.FindByEmailAsync(userId);

            return Ok(item);
        }
        [HttpGet("Upload-User-Profile-Photo")]
        public IActionResult UploadProfilePhoto(IFormFile file)
        {
            //to be implemented

            return Ok("Coming soon");
        }
        
    }
}
