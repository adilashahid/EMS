using EMS.Business.Interfaces;
using EMS.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EMS.Business.Business
{
    public class LoginBusiness : ILoginBusiness
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IConfiguration _configuration;

        public LoginBusiness(ILoginRepository loginRepository, IConfiguration configuration)
        {
            _loginRepository = loginRepository;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = await _loginRepository.LoginAsync(username, password);
            if (user != null && password == user.Password)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CreateUserAsync(string username, string password)
        {
            // Check if the username already exists
            if (await _loginRepository.SignupAsync(username, password))
            {
                return true; // User created successfully
            }
            else
            {
                return false; // User with this username already exists
            }
        }
        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    // Add additional claims if needed
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Set token expiration time
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}