using EMS.DAL.Data;
using EMS.DAL.Interfaces;
using EMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly EMSDbContext _context;
        public TeacherRepository(EMSDbContext context)
        {
            _context = context;
        }
        public async Task<List<Teacher>> GetList()
        {
            return _context.Teachers.ToList();
        }
        public async Task<Teacher> GetById(int id)
        {
            return _context.Teachers.Where(teacher => teacher.TeacherID == id).FirstOrDefault();
        }
        public async Task<Teacher> GetByName(string name)
        {
            return _context.Teachers.Where(teacher => teacher.Name == name).FirstOrDefault();
        }
        public async Task<int> CreateTeacher(Teacher teacher)
        {
            await  _context.Teachers.AddAsync(teacher);
             await   _context.SaveChangesAsync();
             return teacher.TeacherID;

        }
        public async Task<int> UpdateTeacher(Teacher teacher)
        {
             _context.Update(teacher);
             await _context.SaveChangesAsync();
            return teacher.TeacherID;
        }
        public async Task<bool> DeleteTeacher(Teacher teacher)
        {
            _context.Remove(teacher);
            _context.SaveChanges();
            return true;
        }
    }
}
