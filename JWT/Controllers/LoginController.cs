using Microsoft.AspNetCore.Mvc;
using JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWT.Data;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]

        public ActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);
            if(user != null) {
              var token = GenerateToken(user);
                return Ok(token);
            }
            return NotFound("User not found");
        }

        private string GenerateToken(User user)
        {
            try
            {
                var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };
                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private User? Authenticate(UserLogin user)
        {
            var currentUser = UserDb.Users.FirstOrDefault(x => x.Username.ToLower() == user.Username.ToLower()
            && x.Password == user.Password
            );

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
