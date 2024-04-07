using EMS.Business.Interfaces;
using EMS.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "LoginForLocalUsers", Roles ="Superadmin,Admin")]
    public class StudentApiController : ControllerBase
    {
        private readonly IStudentBusiness _studentBusiness;
      
      
        public StudentApiController(IStudentBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
       
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await _studentBusiness.GetStudents();
                if(students == null) return NotFound();
                return Ok(students);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("{rollno:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
     
        public async Task<ActionResult<Student>> GetStudentsByIdAsnc(int rollno)
        {
            try
            {
                if (rollno <= 0)
                {
                
                    return BadRequest();
                }
                var student = await _studentBusiness.GetByIdAsync(rollno);
                if (student == null)
                {
                
                    return NotFound($"The student with rollno{rollno} not found");
                }
                return Ok(student);
            }

            catch (Exception)
            {
                throw;

            }

        }
        [HttpGet]
 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<Student>> GetStudentsByNameAsnc(string name)
        {
            try
            {
                if (name==null)
                {

                    return BadRequest();
                }
                var student = await _studentBusiness.GetByIdName(name);
                if (student == null)
                {

                    return NotFound($"The student with id{name} not found");
                }
                return Ok(student);
            }

            catch (Exception)
            {
                throw;

            }
        }
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            if (student == null)
                return BadRequest();

            var createdStudent = await _studentBusiness.CreatedStudent(student); // await the async method here

            return Ok(createdStudent);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateStudent(Student student)
        {
            try
            {
                if (student.RollNo == null || student == null)
                    return BadRequest();

                var existingStudent = await _studentBusiness.UpdateStudent(student);

                if (existingStudent == null)
                    return NotFound();

                return Ok(existingStudent);
            }
            catch
            {
                return StatusCode(500, "An error occurred while updating the teacher.");
            }

            
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteStudent(int rollno)
        {
            if (rollno == null)
                return BadRequest();

            var student = await _studentBusiness.GetByIdAsync(rollno);

            if (student == null)
                return NotFound();

            
            var deletedSuccessfully = await _studentBusiness.DeleteStudentAsync(student);

            return deletedSuccessfully;
        }

    }
}
