using EMS.DAL.Data;
using EMS.DAL.Interfaces;
using EMS.Entities.Models;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;

namespace EMS.DAL.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly EMSDbContext _context;
        public StudentRepository(EMSDbContext context)
        {
            _context = context;
        }

       
        public async Task<List<Student>> GetList()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetById(int rollno)
        {
            return await _context.Students.Where(student => student.RollNo == rollno).FirstOrDefaultAsync();
        }
        public async Task<Student> GetByName(string name)
        {
            return await _context.Students.Where(student => student.Name == name).FirstOrDefaultAsync();

        }
        public async Task<int> CreateAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student.RollNo;
        }
        public async Task<int> UpdateAsync(Student student)
        {
 
            _context.Update(student);
            await _context.SaveChangesAsync();
            return student.RollNo;
        }
        public async Task<bool> DeleteStudentAsync(Student student) 
        {
            _context.Remove(student);
            await _context.SaveChangesAsync();
            return true;
        }              
    }
}
