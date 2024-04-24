using EMS.Entities.Models;

namespace EMS.DAL.Interfaces
{


    public interface ILoginRepository
        {
        Task<User> LoginAsync(string username, string password);
        Task<bool> SignupAsync(string username, string password);
    }
    }

