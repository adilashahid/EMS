using EMS.DAL.Interfaces;
using EMS.DAL.Repository;
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
        private readonly ILoginRepository _loginRepository;
    
        public LoginController(IConfiguration configuration, ILoginRepository loginRepository)
        {
            _configuration = configuration;
            _loginRepository = loginRepository;
            }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO model)
        {
            if (await _loginRepository.AuthenticateAsync(model.UserName, model.Password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, model.UserName),
                        // Add additional claims if needed
                    }),
                    Expires = DateTime.UtcNow.AddHours(1), // Set token expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { Token = tokenHandler.WriteToken(token) });
            }
            return Unauthorized();
        }
        

        [HttpPost("signup")]
        public async Task<ActionResult> Signup(LoginDTO model)
        {

            if (await _loginRepository.CreateUserAsync(model.UserName, model.Password))
            {
                return Ok("User created successfully");
            }
            return Conflict("Username already exists");
        }

    }
}
       