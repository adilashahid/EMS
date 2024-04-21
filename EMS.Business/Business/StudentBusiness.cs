using EMS.Business.Interfaces;
using EMS.DAL.Interfaces;
using EMS.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.Business.Business
{
    public class StudentBusiness : IStudentBusiness
    {
        private readonly IStudentRepository _studentRepository;
        public StudentBusiness(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _studentRepository.GetList();
        }
        public async Task<Student> GetByIdAsync(int rollno)
        {
            return await _studentRepository.GetById(rollno);
        }
        public async Task<Student> GetByIdName(string name)
        {
            return await _studentRepository.GetByName(name);
        }
        public async Task<int> CreatedStudent(Student student)
        {

            await _studentRepository.CreateAsync(student); 
           return student.RollNo;
        }
        
        public async Task<int> UpdateStudent(Student student)
        {
            var existingstudent = await _studentRepository.GetById(student.RollNo);

            if (existingstudent == null)
            {
                throw new ArgumentNullException($"No Student found with this  RollNo : {student.RollNo}");
            }
            existingstudent.MobileNo = student.MobileNo;
            existingstudent.Name = student.Name;
            existingstudent.FatherName = student.FatherName;
            existingstudent.Address = student.Address;
            existingstudent.ClassName = student.ClassName;
            existingstudent.DOB = student.DOB;
            existingstudent.Email = student.Email;
            existingstudent.Fee = student.Fee;
            await _studentRepository.Commit();
            return existingstudent.RollNo;
        }
        public async Task<bool> DeleteStudentAsync(Student student)
        {
         
            await _studentRepository.DeleteStudentAsync(student);
            return true;
        }
    }
}
