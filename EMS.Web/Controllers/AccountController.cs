using EMS.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace EMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
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
            var response = await _httpClient.PostAsync("https://localhost:7141/api/Login",
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

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Index", "Home");
        }
    }
}
