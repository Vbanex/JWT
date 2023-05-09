using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using JWT.Models;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController: ControllerBase
    {
        [HttpGet]
        [Route("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndPoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi {currentUser.Username}, You are an {currentUser.Role}");
        }

        private User GetCurrentUser() {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null) {
            var userClaims = identity.Claims;
                return new User {
                Username = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value,
                Role = userClaims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                };
            }
            return null;
        }
    }
}
