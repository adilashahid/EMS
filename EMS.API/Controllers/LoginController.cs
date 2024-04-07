using EMS.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EMS.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide username and password");
            }
            LoginResponseDTO response = new() { UserName = model.UserName };
            string audience = string.Empty;
            string issuer = string.Empty;
            byte[] key = null;
            if (model.Policy == "Local")
            {
                issuer = _configuration.GetValue<string>("LocalIssur");
                audience = _configuration.GetValue<string>("LocalAudience");
                key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTsecretForLocal"));
            }
            
            if (model.UserName == "Adila" && model.Password == "Adila123")
            {


                var tokenhandler = new JwtSecurityTokenHandler();
                var Tokendiscripter = new SecurityTokenDescriptor()
                {
                    Issuer = issuer,
                    Audience = audience,
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.UserName),
                        new Claim(ClaimTypes.Role, "Admin")
                    }),
                    Expires = DateTime.Now.AddDays(4),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };
                var token = tokenhandler.CreateToken(Tokendiscripter);
                response.Token = tokenhandler.WriteToken(token);
            }
            else
            {
                return Ok("Invalid username and Password");
            }
            return Ok(response);
        }


    }
}

