using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public string Index()
        {
            return "hello";
        }

        [HttpGet]
        [Route("message")]
        public string Message()
        {
            return "You need authorization for this";
        }
    }
}
