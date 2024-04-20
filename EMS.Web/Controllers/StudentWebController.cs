using ems.web.services;
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

        public StudentWebController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, JwtTokenService jwtTokenService)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _jwtTokenService = jwtTokenService;
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
                        return RedirectToAction(nameof(Index));
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
                        if (responseData != null)
                        {
                            return View(responseData);
                        }
                        else
                        {
                            // Handle null response data
                            return View("Error");
                        }
                    }
                    else
                    {
                        // Handle non-success status code
                        return View("Error");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                
                return View("Error");
            }
            catch (Exception ex)
            {
              
                return View("Error");
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
                        // Handle error
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
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
                using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
                {
                    HttpResponseMessage response = await httpClient.PutAsJsonAsync("api/StudentApi/UpdateStudent", student);

                    if (response.IsSuccessStatusCode)
                    {
                        // Redirect to index action after successful update
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle error
                        return View("Error");
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and handle it appropriately
                return View("Error");
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
                        // Redirect to index action after successful deletion
                        return RedirectToAction(nameof(Index));
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

    }
}

