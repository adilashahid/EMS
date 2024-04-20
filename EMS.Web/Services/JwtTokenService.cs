using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace ems.web.services
{
    public class JwtTokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtTokenService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<HttpClient> GetHttpClientWithJwtAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            // Retrieve JWT token from session
            var session = _httpContextAccessor.HttpContext.Session;
            var token = session.GetString("JWToken");

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
    }
}
