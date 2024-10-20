using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers
{
    [ApiController]
    public class ErrorController_ : ControllerBase
    {
        [Route("/error")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HandleError()
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = feature?.Error;

            // Log the exception or perform any required action

            return Problem(detail: exception?.Message, statusCode: 500);
        }
    }
}
