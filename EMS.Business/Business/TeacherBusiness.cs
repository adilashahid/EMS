using EMS.Business.Interfaces;
using EMS.DAL.Interfaces;
using EMS.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Business.Business
{
    public class TeacherBusiness:ITeacherBusiness
    {
        private readonly ITeacherRepository _teacherrepository;
        public TeacherBusiness(ITeacherRepository teacherrepository) 
        {
            _teacherrepository = teacherrepository;
        }
        public async Task<List<Teacher>> GetTeachersAsync()
        {
            return  await _teacherrepository.GetList();
        }
        public async Task<Teacher> GetByIdAsnc(int id)
        {
            return await _teacherrepository.GetById(id);
        }
        public async Task<Teacher> GetByNameAsync(string name)
        {
            return await _teacherrepository.GetByName(name);
        }
        public async Task<int> CreateTeacherAsync(Teacher teacher)
        {
            Teacher teachers = new Teacher
            {
                TeacherID = teacher.TeacherID,
                Name = teacher.Name,
                FatherName = teacher.FatherName,
                Email = teacher.Email,
                Address = teacher.Address,
                DOB = teacher.DOB,
                MobileNo = teacher.MobileNo,
                Salary = teacher.Salary
            };
            await  _teacherrepository.CreateTeacher(teachers);
            return teachers.TeacherID;
        }
        public async Task<int> UpdateTeacherAsync(Teacher teacher)
        {
            var ExistingTeacher= await _teacherrepository.GetById(teacher.TeacherID);
            if(ExistingTeacher==null)
            {
                throw new ArgumentNullException($"No Teacher found with this  ID : {teacher.TeacherID}");
            }
            ExistingTeacher.Name=teacher.Name;
            ExistingTeacher.Salary=teacher.Salary;
            ExistingTeacher.FatherName=teacher.FatherName;
            ExistingTeacher.Address=teacher.Address;
            ExistingTeacher.DOB=teacher.DOB;
            ExistingTeacher.Email=teacher.Email;
           await _teacherrepository.UpdateTeacher(ExistingTeacher);
            return ExistingTeacher.TeacherID;
        }
        public async Task<bool> DeleteTeacherAsync(Teacher teacher)
        {
            await _teacherrepository.DeleteTeacher(teacher);
            return true;
        }
    }
}
