
using CourseCenter.WebUI.DTOs.AuthDtos;
using System.IdentityModel.Tokens.Jwt;
using NuGet.Common;
using System.Security.Claims;

namespace CourseCenter.WebUI.Helpers
{
    public class RefreshTokenService(IHttpContextAccessor _httpContextAccessor, IServiceProvider _serviceProvider) : IRefreshTokenService
    {
        public string GetAccessToken()
        {
            return _httpContextAccessor.HttpContext.Items["AccessToken"] as string
               ?? _httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
        }

        public UserFromTokenDto GetUserFromToken(string accessToken)
        {
            UserFromTokenDto userFromTokenDto = new UserFromTokenDto();

            try
            {
                // JwtSecurityTokenHandler ile token'ı çöz
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(accessToken);

                var payload = jwtToken.Payload;
                if (payload != null)
                {
                    userFromTokenDto.UserId = payload[ClaimTypes.NameIdentifier]?.ToString();
                    userFromTokenDto.FullName = payload[JwtRegisteredClaimNames.Name]?.ToString();
                    userFromTokenDto.Email = payload[JwtRegisteredClaimNames.Email]?.ToString();
                }

                return userFromTokenDto;
            }
            catch (Exception ex)
            {
                return userFromTokenDto;
            }
            
        }

        public async Task<bool> RefreshTokensAsync()
        {
            RequestTokenDto requestTokenDto = new()
            {
                AccessToken = _httpContextAccessor.HttpContext.Request.Cookies["AccessToken"] ?? "",
                RefreshToken = _httpContextAccessor.HttpContext.Request.Cookies["RefreshToken"]
            };

            if (string.IsNullOrEmpty(requestTokenDto.RefreshToken))
                return false;

            var httpClientService = _serviceProvider.GetService<IHttpClientService>();
            ResponseTokenDto tokenResponse = await httpClientService.SendRequestAsync<RequestTokenDto, ResponseTokenDto>(HttpMethod.Post, "auths/refreshtokencheck", requestTokenDto);

            if (tokenResponse == null)
                return false;

            RemoveTokensCookies();
            SaveTokensCookies(tokenResponse.AccessToken, tokenResponse.RefreshToken);

            return true;
        }

        public void RemoveTokensCookies()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("AccessToken", new CookieOptions
            {
                HttpOnly = true,                           // JavaScript ile erişilemez
                Secure = true,                             // HTTPS zorunlu
                Expires = DateTime.UtcNow.AddDays(-1),     // Çerezin süresi geçmiş bir tarihe ayarlanır
                SameSite = SameSiteMode.None,              // CSRF korunması için // Strict,Lax,None 
                Path = "/",                                // Çerezin tüm yollar için geçerli olması
                //Domain = "localhost:7102/"               // Çerezin domaini (istenirse)
            });

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("RefreshToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(-1),
                SameSite = SameSiteMode.None,
                Path = "/",
                //Domain = "localhost:7102/"
            });
        }

        public void SaveTokensCookies(string accessToken, string refreshToken)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
            {
                HttpOnly = true,                           // JavaScript ile erişilemez
                Secure = true,                             // HTTPS zorunlu
                Expires = DateTime.UtcNow.AddMinutes(15),  // Token süresi
                SameSite = SameSiteMode.None,              // CSRF korunması için // Strict,Lax,None 
                Path = "/",                                // Çerezin tüm yollar için geçerli olması
                //Domain = "localhost:7102/"               // Çerezin domaini (istenirse)
            });
            _httpContextAccessor.HttpContext.Items["AccessToken"] = accessToken;

            _httpContextAccessor.HttpContext.Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7),
                SameSite = SameSiteMode.None,
                Path = "/",
                //Domain = "localhost:7102/"
            });
            _httpContextAccessor.HttpContext.Items["RefreshToken"] = refreshToken;
        }
    }
}
