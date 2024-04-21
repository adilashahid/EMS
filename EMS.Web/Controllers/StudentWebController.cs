using Azure;
using ems.web.services;
using EMS.Entities.Models;
using EMS.Web.Models;
//using EMS.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace EMS.Web.Controllers
{

    public class StudentWebController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly JwtTokenService _jwtTokenService;
        private readonly ILogger<StudentWebController> _logger;

        public StudentWebController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, JwtTokenService jwtTokenService, ILogger<StudentWebController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }



        public async Task<IActionResult> Index()
        {
            try
            {

                var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync();

                {
                    HttpResponseMessage response = await httpClient.GetAsync("api/StudentApi/GetStudents");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsAsync<List<Student>>();
                        return View("Index", responseData);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
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
                using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
                {
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/StudentApi/CreateStudent", student);

                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to index action after successful creation
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> Details(int rollno)
        {
            try
            {
                using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
                {
                    HttpResponseMessage response = await httpClient.GetAsync($"api/StudentApi/GetStudentsByIdAsnc/{rollno}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsAsync<Student>();
                        return View(responseData);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving teacher details.");
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: StudentWeb/Edit/5
        public async Task<IActionResult> Edit(int rollno)
        {
            try
            {
                using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
                {
                    HttpResponseMessage response = await httpClient.GetAsync($"api/StudentApi/GetStudentsByIdAsnc/{rollno}");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsAsync<Student>();
                        return View(responseData);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving teacher for editing.");
                return RedirectToAction("Error", "Home");
            }
        }
        // POST: StudentWeb/Update
        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            try
            {
                using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
                {
                    HttpResponseMessage response = await httpClient.PutAsJsonAsync("api/StudentApi/UpdateStudent", student);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the teacher.");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int rollno)
        {
            try
            {
                using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync($"api/StudentApi/DeleteStudent?rollno={rollno}");

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Log the error response
                        _logger.LogError("Delete action failed with status code {StatusCode}", response.StatusCode);
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while deleting the teacher.");
                return RedirectToAction("Error", "Home");
            }
        }


    }
}
