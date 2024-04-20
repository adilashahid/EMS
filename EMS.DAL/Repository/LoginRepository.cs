using EMS.DAL.Data;
using EMS.DAL.Interfaces;
using EMS.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMS.DAL.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly EMSDbContext _context;
        public LoginRepository(EMSDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> CreateUserAsync(string username, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
            {
                return false; // User with this username already exists
            }

            // Hash the password using a secure password hashing algorithm (e.g., BCrypt)
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            // Save the user to the database
            var user = new User { Username = username, PasswordHash = hashedPassword };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }



        private string HashPassword(string password)
        {
            return password;
        }

        private bool VerifyPassword(string inputPassword, string hashedPassword)
        {

            return inputPassword == hashedPassword;
        }
    }
}
