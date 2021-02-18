using Microsoft.AspNetCore.Mvc;

namespace BooksWebApi.Controllers
{
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Swagger()
        {
            return Redirect("/swagger");
        }
    }
}
