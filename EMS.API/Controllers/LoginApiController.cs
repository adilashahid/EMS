using EMS.Business.Interfaces;
using EMS.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginBusiness _loginBusiness;
    
        public LoginApiController(IConfiguration configuration, ILoginBusiness loginBusiness)
        {
            _configuration = configuration;
           _loginBusiness = loginBusiness;
            }

        [HttpPost("login")]
        public async Task<ActionResult> Login(User model)
        {
            if (await _loginBusiness.AuthenticateAsync(model.Username, model.Password))
            {
                var token = _loginBusiness.GenerateToken(model.Username);
                return Ok(new { Token = token });
            }
            return Unauthorized();
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Signup(User model)
        {

            if (await _loginBusiness.CreateUserAsync(model.Username, model.Password ))
            {
                return Ok("User created successfully");
            }
            return Conflict("Username already exists");
        }

    }
}
       