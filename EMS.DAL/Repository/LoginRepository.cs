using EMS.DAL.Data;
using EMS.DAL.Interfaces;
using EMS.Entities;
using EMS.Entities.Models;
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
        public async Task<User> LoginAsync(string username, string password)
        {
           return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            
        }
        public async Task<bool> SignupAsync(string username, string password)
        {
            if (await _context.Users.AnyAsync(u => u.Username == username))
            {
                return false; // User with this username already exists
            }

            // Save the user to the database
            var user = new User { Username = username, Password = password };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }




    }
}
