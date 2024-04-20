using EMS.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace EMS.Web.Controllers
{
    public class TeacherWebController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<TeacherWebController> _logger;
        public TeacherWebController(IHttpClientFactory httpClientFactory, ILogger<TeacherWebController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;

        }

        private async Task<HttpClient> GetHttpClientWithJwtAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            // Retrieve JWT token from session
            var token = HttpContext.Session.GetString("JWToken");

            // Check if token exists
            if (!string.IsNullOrEmpty(token))
            {
                // Add token to request headers
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // Set hardcoded API base URL
            httpClient.BaseAddress = new Uri("https://localhost:7141/");

            return httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            
            {
                using (var httpClient = await GetHttpClientWithJwtAsync())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7141/");
                    HttpResponseMessage respose = await httpClient.GetAsync("api/TeacherApi/GetTeachers");
                    if (respose.IsSuccessStatusCode)
                    {
                        var responsedata = await respose.Content.ReadAsAsync<List<Teacher>>();
                        return View("Index", responsedata);
                    }
                    else
                    {
                        return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            
                {
                    using (var httpClient = await GetHttpClientWithJwtAsync())
                    {
                        HttpResponseMessage response = await httpClient.GetAsync($"api/TeacherApi/GetById/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsAsync<Teacher>();
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
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Teacher teacher)
        {
            try
            
                {
                    using (var httpClient = await GetHttpClientWithJwtAsync())
                    {
                        HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/TeacherApi/CreateTeachers", teacher);

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
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                using (var httpClient = await GetHttpClientWithJwtAsync())
                {
                   

                    HttpResponseMessage response = await httpClient.GetAsync($"api/TeacherApi/GetById/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsAsync<Teacher>();
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



        [HttpPost]
        public async Task<IActionResult> Edit(Teacher teacher)
       {
            try
            {
                using (var httpClient = await GetHttpClientWithJwtAsync())
                {
                    HttpResponseMessage response = await httpClient.PutAsJsonAsync("api/TeacherApi/UpdateTeachers", teacher);

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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (var httpClient = await GetHttpClientWithJwtAsync())
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync($"api/TeacherApi/DeleteTeacher?id={id}");

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

    


