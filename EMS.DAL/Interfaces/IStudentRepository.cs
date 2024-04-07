using EMS.Entities.Models;
using NPOI.SS.Formula.Functions;
using System.Linq.Expressions;


namespace EMS.DAL.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetList();
        Task<Student> GetById(int rollno);
        Task<Student> GetByName(string name);
        Task<int> CreateAsync(Student student);
        Task<int> UpdateAsync(Student student);
        Task<bool> DeleteStudentAsync(Student student);

    }
}
