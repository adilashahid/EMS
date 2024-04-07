using EMS.Entities.Models;

namespace EMS.Business.Interfaces
{
    public interface IStudentBusiness
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetByIdAsync(int rollno);
        Task<Student> GetByIdName(string name);
        Task<int> CreatedStudent(Student student);
        Task<int> UpdateStudent(Student student);
        Task<bool> DeleteStudentAsync(Student student);
    }
}
