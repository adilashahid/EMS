using EMS.Entities.Models;
using EMS.Web.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EMS.Web.Controllers
{

    public class StudentWebController : Controller
    {
        public StudentWebController()
        {
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var students = await HttpClientHelper.SendHttpRequest<object, List<Student>>(APIEndpoints.Students
                    .GetStudents, HttpMethod.Get, null, token);
                return View("Index", students);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var studentResponse = await HttpClientHelper.SendHttpRequest<Student, int>(APIEndpoints.Students
                    .CreateStudent, HttpMethod.Post, student, token);

                if (studentResponse > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        public async Task<IActionResult> Details(int rollno)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var student = await HttpClientHelper.SendHttpRequest<object, Student>(APIEndpoints.Students
                    .GetStudentsById(rollno), HttpMethod.Get, null, token);

                if (student is not null)
                {
                    return View(student);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception)
            {
                // Log and handle other exceptions
                return View("Error");
            }
        }

        // GET: StudentWeb/Edit/5
        public async Task<IActionResult> Edit(int rollno)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var student = await HttpClientHelper.SendHttpRequest<object, Student>(APIEndpoints.Students
                    .GetStudentsById(rollno), HttpMethod.Get, null, token);

                if (student is not null)
                {
                    return View(student);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception)
            {
                // Log the exception and handle it appropriately
                return View("Error");
            }
        }
        // POST: StudentWeb/Update
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var studentResponse = await HttpClientHelper.SendHttpRequest<Student, int>(APIEndpoints.Students
                    .UpdateStudent, HttpMethod.Put, student, token);

                if (studentResponse > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int rollno)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var studentDeleteResp = await HttpClientHelper.SendHttpRequest<object, bool>(APIEndpoints.Students
                    .GetStudentsById(rollno), HttpMethod.Delete, null, token);

                if (!studentDeleteResp)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

    }
}

