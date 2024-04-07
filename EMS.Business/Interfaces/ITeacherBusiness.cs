using EMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Business.Interfaces
{
    public interface ITeacherBusiness
    {
        Task<List<Teacher>> GetTeachersAsync();
        Task<Teacher> GetByIdAsnc(int id);
        Task<Teacher> GetByNameAsync(string name);
        Task<int> CreateTeacherAsync(Teacher teacher);
        Task<int> UpdateTeacherAsync(Teacher teacher);
        Task<bool> DeleteTeacherAsync(Teacher teacher);
    }
}
