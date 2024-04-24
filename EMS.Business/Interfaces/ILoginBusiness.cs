using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Business.Interfaces
{
    public interface ILoginBusiness
    {
        Task<bool> AuthenticateAsync(string username, string password);
        Task<bool> CreateUserAsync(string username, string password);
        string GenerateToken(string username);
    }
}
