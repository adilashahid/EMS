using EMS.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;

namespace EMS.Web.Controllers
{

    public class StudentWebController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public StudentWebController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        private string GenerateJwtToken(string jwtSecret)
        {
          
            // Implement logic to generate JWT token using the provided secret key
            // Example implementation using System.IdentityModel.Tokens.Jwt:
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
          
          ;

            var token = new JwtSecurityToken(
               
                expires: DateTime.UtcNow.AddHours(1), // Token expiry time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

       

        private async Task<HttpClient> GetHttpClientWithJwtAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:7141/");

            //// Retrieve JWT secret key from configuration
            //string jwtSecret = _configuration["JWTsecretForLocal"];


            var token = HttpContext.Session.GetString("JWToken");
            //// Set JWT token in the Authorization header
            //string jwtToken = GenerateJwtToken(jwtSecret); // Implement a method to generate JWT token
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return httpClient;
        }


        public async Task<IActionResult> Index()
        {
            try
            {

                using (var httpClient = await GetHttpClientWithJwtAsync())
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
        //private async Task<string> GetJwtTokenAsync()
        //{
        //    // Retrieve the JWT token from the HttpContext session
        //    var token = await Task.FromResult(_httpContextAccessor.HttpContext.Session.GetString("JWToken"));
        //    return token;
        //}
        //private async Task<HttpClient> GetHttpClientWithTokenAsync()
        //{
        //    var httpClient = _httpClientFactory.CreateClient();
        //    httpClient.BaseAddress = new Uri("https://localhost:7141/");

        //    // Set JWT token in the Authorization header
        //    var token = await GetJwtTokenAsync();
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //    }

        //    return httpClient;
        //}

        //public async Task<IActionResult> Index()
        //{
        //    try
        //    {
        //        using (var httpClient = await GetHttpClientWithTokenAsync())
        //        {
        //            // Make a GET request to the API endpoint
        //            HttpResponseMessage response = await httpClient.GetAsync("api/StudentApi/GetStudents");

        //            if (response.IsSuccessStatusCode)
        //            {
        //                // Deserialize the JSON response
        //                var responseData = await response.Content.ReadAsAsync<List<Student>>();

        //                // Process the data as needed
        //                return View("Index", responseData);
        //            }
        //            else
        //            {
        //                // Handle the error, log, or display an appropriate message
        //                return View("Error");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception and handle it appropriately
        //        return View("Error");
        //    }
        //}


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
                using (var httpClient = await GetHttpClientWithJwtAsync())
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
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7141/");

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
                // Log HTTP request exception
                // This could occur due to network issues or server not responding
                // Log or handle the exception as appropriate
                return View("Error");
            }
            catch (Exception ex)
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
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7141/");

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
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7141/");

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
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.BaseAddress = new Uri("https://localhost:7141/");

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

