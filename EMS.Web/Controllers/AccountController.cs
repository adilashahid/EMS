//using EMS.Entities.Models;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.IdentityModel.Tokens;

//namespace EMS.Web.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public AccountController(IHttpClientFactory httpClientFactory)
//        {
//            _httpClient = httpClientFactory.CreateClient();
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> Login(LoginDTO model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest("Invalid input.");
//            }

//            try
//            {
//                var tokenResponse = await GetAuthToken(model);
//                var tokenDto = JsonConvert.DeserializeObject<LoginResponseDTO>(tokenResponse);
//                if (!string.IsNullOrEmpty(tokenDto.Token))
//                {
//                    HttpContext.Session.SetString("JWToken", tokenDto.Token); // Set token in session
//                    return RedirectToAction("Index", "Home");
//                }
//                else
//                {
//                    ViewBag.Message = "Invalid credentials.";
//                    return View();
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception
//                ViewBag.Message = "An error occurred while processing your request.";
//                return View();
//            }
//        }


//        private async Task<string> GetAuthToken(LoginDTO model)
//        {
//            var response = await _httpClient.PostAsync("https://localhost:7141/api/Login",
//                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

//            if (response.IsSuccessStatusCode)
//            {
//                return await response.Content.ReadAsStringAsync();
//            }
//            else
//            {
//                return null;
//            }
//        }

//        public IActionResult Logout()
//        {
//            HttpContext.Session.Remove("JWToken");
//            return RedirectToAction("Index", "Home");
//        }
//    }
//}
// AccountController.cs
// AccountController.cs
using ems.web.services;
using EMS.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountController(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;
        _httpClient = httpClientFactory.CreateClient();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input.");
        }
        try
        {
            var tokenResponse = await GetAuthToken(model);
            var tokenDto = JsonConvert.DeserializeObject<LoginResponseDTO>(tokenResponse);
            if (!string.IsNullOrEmpty(tokenDto.Token))
            {
                HttpContext.Session.SetString("JWToken", tokenDto.Token); // Set token in session
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid credentials.";
                return View();
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.Message = "An error occurred while processing your request.";
            return View();
        }
    }

    private async Task<string> GetAuthToken(LoginDTO model)
    {
        var response = await _httpClient.PostAsync("https://localhost:7141/api/Login/login",
            new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            return null;
        }
    }

   

    [HttpGet]
    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Signup(LoginDTO model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid input.");
        }

        try
        {
            var response = await _httpClient.PostAsync("https://localhost:7141/api/Login/signup",
                new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = "Error while signing up. Please try again later.";
                return View();
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.Message = "An error occurred while processing your request.";
            return View();
        }
    
}
}
