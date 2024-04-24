using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace EMS.Web.Helpers;

public static class HttpClientHelper
{
    public static async Task<TResponse> SendHttpRequest<TRequest, TResponse>(
        string url,
        HttpMethod method,
        TRequest requestData,
        string? authToken = null,
        bool sslDisable = true
    )
    {
        HttpResponseMessage response = new HttpResponseMessage();
        try
        {
            HttpRequestMessage request = new HttpRequestMessage(method, url);
            // Set the Authorization header based on the authentication scheme
            if (!string.IsNullOrEmpty(authToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }

            if (requestData != null && method != HttpMethod.Get) // Assuming GET requests do not send data in the body
            {
                string json = JsonConvert.SerializeObject(requestData,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.None,
                        DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                    });
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            using HttpClientHandler handler = new();
            if (sslDisable) // Bypass SSL validation
                handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

            using (HttpClient client = new(handler))
            {
                client.BaseAddress = new Uri("https://localhost:7141/");
                response = await client.SendAsync(request);
            }

            if (response.IsSuccessStatusCode)
            {
                string responseJson = await response.Content.ReadAsStringAsync();
                if (responseJson is null) return default!;
                TResponse? responseData = JsonConvert.DeserializeObject<TResponse>(responseJson);
                return responseData ?? default!;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new NotSupportedException("Unauthorized");
            }
            else
                throw new NotSupportedException($"{response.StatusCode} {url} {response.ReasonPhrase}");
        }
        catch (Exception ex)
        {
            if (ex.Message == "Unauthorized")
                throw;
            throw new HttpRequestException($"HTTP request failed with status code {response?.StatusCode}: {response?.ReasonPhrase}", ex);
        }
    }
}
