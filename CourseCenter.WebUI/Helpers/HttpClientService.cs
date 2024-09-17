using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Collections.Generic;
using CourseCenter.WebUI.DTOs.AuthDtos;

namespace CourseCenter.WebUI.Helpers
{
    public class HttpClientService(IHttpClientFactory _httpClientFactory, IHttpContextAccessor _httpContextAccessor, IConfiguration _configuration, IServiceProvider _serviceProvider) : IHttpClientService
    {
        public HttpClient CreateHttpClient()
        {
            HttpClient client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseUrl:BackendBaseUrl"]);

            // Token'ı Authorization header'a ekle
            var refreshTokenService = _serviceProvider.GetService<IRefreshTokenService>();
            string accessToken = refreshTokenService.GetAccessToken();            
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            return client;
        }

        public async Task<TResponse> SendRequestAsync<TRequest, TResponse>(HttpMethod method, string apiUrl, TRequest requestDataObject)
        {
            HttpClient client = CreateHttpClient();

            HttpRequestMessage request = new()
            {
                RequestUri = new Uri(apiUrl, UriKind.RelativeOrAbsolute),
                Method = method
            };

            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                if (requestDataObject != null)
                {
                    var content = new StringContent(JsonConvert.SerializeObject(requestDataObject), Encoding.UTF8, "application/json");
                    request.Content = content;
                }
            }

            HttpResponseMessage response = await client.SendAsync(request);            

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TResponse>(responseData);
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshTokenService = _serviceProvider.GetService<IRefreshTokenService>();
                if (await refreshTokenService.RefreshTokensAsync())
                    return await SendRequestAsync<TRequest, TResponse>(method, apiUrl, requestDataObject);
                else
                    _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }

            if (response.StatusCode == HttpStatusCode.Forbidden)
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

            return default(TResponse);
        }
    }
}
