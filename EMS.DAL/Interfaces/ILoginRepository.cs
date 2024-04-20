using EMS.Entities;
using EMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Interfaces
{

    
        public interface ILoginRepository
        {
        Task<bool> AuthenticateAsync(string username, string password);
        Task<bool> CreateUserAsync(string username, string password);
    }
    }

