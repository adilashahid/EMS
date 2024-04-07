using EMS.Business.Interfaces;
using EMS.Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "LoginForLocalUsers", Roles = "Superadmin,Admin")]
    public class TeacherApiController : ControllerBase
    {
        private readonly ITeacherBusiness _teacherBusiness;
        public TeacherApiController(ITeacherBusiness teacherBusiness)
        {
            _teacherBusiness = teacherBusiness;
        }
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            try
            {
                var teacher = await _teacherBusiness.GetTeachersAsync();
                if (teacher == null)
                    return NotFound();
                return Ok(teacher);    
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Teacher>> GetByIdAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var teacher =await  _teacherBusiness.GetByIdAsnc(id);
                if (teacher == null)
                    return NotFound($"Teacher with thid ID {id} not Found");
                return Ok(teacher);
            }
           catch(Exception)
            {
                throw;
            }
            
        }
        [HttpGet]
        public async Task<ActionResult<Teacher>> GetByNameAsync(string name)
        {
            try
            {
                if (name== null)
                {
                    return BadRequest();
                }
                var teacher = await _teacherBusiness.GetByNameAsync(name);
                if (teacher == null)
                    return NotFound($"Teacher with thid ID {name} not Found");
                return Ok(teacher);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPost]
        public async Task<ActionResult<Teacher>> CreateTeachers(Teacher teacher)
        {
            if (teacher == null)
                return BadRequest();
            var existingdata=await _teacherBusiness.CreateTeacherAsync(teacher);  
            return Ok(existingdata);
        }
        [HttpPut]
        
        public async Task<ActionResult> UpdateTeachers(Teacher teacher,int id)
        {
            try
            {
                if (teacher == null || teacher.TeacherID == null) // Check if teacher is null first
                    return BadRequest();

                var updatedData = await _teacherBusiness.UpdateTeacherAsync(teacher);

                if (updatedData == null)
                    return NotFound();

                return Ok(updatedData);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while updating the teacher.");
            }
        }


    
    [HttpDelete]
        public async Task<ActionResult<bool>> DeleteTeacher(int id)
        {
            if (id == null)
                return BadRequest();

            var teacher = await _teacherBusiness.GetByIdAsnc(id);   

            if (teacher == null)
                return NotFound();


            var deletedSuccessfully = await _teacherBusiness.DeleteTeacherAsync(teacher);

            return deletedSuccessfully;
        }
    }
}
