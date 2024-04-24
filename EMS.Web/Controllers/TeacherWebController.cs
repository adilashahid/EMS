using ems.web.services;
using EMS.Entities.Models;
using EMS.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
namespace EMS.Web.Controllers
{
    public class TeacherWebController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly JwtTokenService _jwtTokenService;
        private readonly ILogger<TeacherWebController> _logger;

        public TeacherWebController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, JwtTokenService jwtTokenService, ILogger<TeacherWebController> logger)
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
                var token = HttpContext.Session.GetString("JWToken");
                var teachers = await HttpClientHelper.SendHttpRequest<object, List<Teacher>>(APIEndpoints.Teachers
                    .GetTeachers, HttpMethod.Get, null, token);
                return View("Index", teachers);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var teacher = await HttpClientHelper.SendHttpRequest<object, Student>(APIEndpoints.Teachers
                    .GetTeacherById(id), HttpMethod.Get, null, token);

                if (teacher is not null)
                {
                    return View(teacher);
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
        //public async Task<IActionResult> Index()
        //{
        //    try

        //    {
        //        using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
        //        {
        //            httpClient.BaseAddress = new Uri("https://localhost:7141/");
        //            HttpResponseMessage respose = await httpClient.GetAsync("api/TeacherApi/GetTeachers");
        //            if (respose.IsSuccessStatusCode)
        //            {
        //                var responsedata = await respose.Content.ReadAsAsync<List<Teacher>>();
        //                return View("Index", responsedata);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Error", "Home");
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


        //[HttpGet]
        //public async Task<IActionResult> Details(int id)
        //{
        //    try

        //        {
        //            using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
        //            {
        //                HttpResponseMessage response = await httpClient.GetAsync($"api/TeacherApi/GetById/{id}");

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var responseData = await response.Content.ReadAsAsync<Teacher>();
        //                return View(responseData);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Error", "Home");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while retrieving teacher details.");
        //        return RedirectToAction("Error", "Home");
        //    }
        //}
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }
        public async Task<IActionResult> Create(Teacher teacher)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var teacherResponse = await HttpClientHelper.SendHttpRequest<Teacher, int>(APIEndpoints.Teachers
                    .CreateTeachers, HttpMethod.Post, teacher, token);

                if (teacherResponse > 0)
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

        //[HttpPost]
        //public async Task<IActionResult> Create(Teacher teacher)
        //{
        //    try

        //        {
        //            using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
        //            {
        //                HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/TeacherApi/CreateTeachers", teacher);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                // Redirect to index action after successful creation
        //                return RedirectToAction(nameof(Index));
        //            }
        //            else
        //            {
        //                return RedirectToAction("Error", "Home");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return RedirectToAction("Error", "Home");
        //    }
        //}
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    try
        //    {
        //        using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
        //        {


        //            HttpResponseMessage response = await httpClient.GetAsync($"api/TeacherApi/GetById/{id}");

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var responseData = await response.Content.ReadAsAsync<Teacher>();
        //                return View(responseData);
        //            }
        //            else
        //            {
        //                return RedirectToAction("Error", "Home");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while retrieving teacher for editing.");
        //        return RedirectToAction("Error", "Home");
        //    }
        //}
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var teacher = await HttpClientHelper.SendHttpRequest<object, Teacher>(APIEndpoints.Teachers
                    .GetTeacherById(id), HttpMethod.Get, null, token);

                if (teacher is not null)
                {
                    return View(teacher);
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


        // [HttpPost]
        // public async Task<IActionResult> Edit(Teacher teacher)
        //{
        //     try
        //     {
        //         using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
        //         {
        //             HttpResponseMessage response = await httpClient.PutAsJsonAsync("api/TeacherApi/UpdateTeachers", teacher);

        //             if (response.IsSuccessStatusCode)
        //             {
        //                 return RedirectToAction("Index");
        //             }
        //             else
        //             {
        //                 return RedirectToAction("Error", "Home");
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "An error occurred while updating the teacher.");
        //         return RedirectToAction("Error", "Home");
        //     }
        // }
        [HttpPost]
        public async Task<IActionResult> Edit(Teacher teacher)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var teacherResponse = await HttpClientHelper.SendHttpRequest<Teacher, int>(APIEndpoints.Teachers
                    .UpdateTeachers, HttpMethod.Put, teacher, token);

                if (teacherResponse > 0)
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

        //[HttpPost]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        using (var httpClient = await _jwtTokenService.GetHttpClientWithJwtAsync())
        //        {
        //            HttpResponseMessage response = await httpClient.DeleteAsync($"api/TeacherApi/DeleteTeacher?id={id}");

        //            if (response.IsSuccessStatusCode)
        //            {
        //                return RedirectToAction("Index");
        //            }
        //            else
        //            {
        //                // Log the error response
        //                _logger.LogError("Delete action failed with status code {StatusCode}", response.StatusCode);
        //                return RedirectToAction("Error", "Home");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception
        //        _logger.LogError(ex, "An error occurred while deleting the teacher.");
        //        return RedirectToAction("Error", "Home");
        //    }
        //}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var token = HttpContext.Session.GetString("JWToken");
                var teacherDeleteResp = await HttpClientHelper.SendHttpRequest<object, bool>(APIEndpoints.Teachers
                    .DeleteTeacher(id), HttpMethod.Delete, null, token);

                if (teacherDeleteResp)
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

    


