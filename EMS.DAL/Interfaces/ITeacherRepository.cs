using EMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Interfaces
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetList();
        Task<Teacher> GetById(int id);
        Task<Teacher> GetByName(string Name);
        Task<int> CreateTeacher(Teacher teacher);
        Task<int> UpdateTeacher(Teacher teacher);
        Task<bool> DeleteTeacher(Teacher teacher);
    }
}
