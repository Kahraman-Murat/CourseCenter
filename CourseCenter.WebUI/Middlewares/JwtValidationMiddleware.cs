using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Web;

namespace CourseCenter.WebUI.Middlewares
{
    public class JwtValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRefreshTokenService _refreshTokenService;

        public JwtValidationMiddleware(RequestDelegate next, IRefreshTokenService refreshTokenService)
        {
            _next = next;
            _refreshTokenService = refreshTokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // JwtValidationMiddleware'in aktif olacagi yollari belirle
            var activePaths = new[] 
            {
                "/Admin",
                "/Teacher",
                "/Student",
                "/Area/Controller/Action"
            }; 

            // Belirlilen Yol ise
            if (activePaths.Any(path => context.Request.Path.StartsWithSegments(path)))
            {
                // Ulaşılmak istenen URL'yi al
                var returnUrl = HttpUtility.UrlEncode(context.Request.Path + context.Request.QueryString);

                var accessToken1 = context.Request.Cookies["AccessToken"];
                var accessToken = _refreshTokenService.GetAccessToken();
                var refreshToken = context.Request.Cookies["RefreshToken"];

                Console.WriteLine("-----------------------" + context.Request.Path);
                Console.WriteLine("-----------------------" + accessToken1);
                Console.WriteLine("-----------------------" + accessToken);

                // Access token yoksa veya süresi dolmuşsa kontrol et
                if (string.IsNullOrEmpty(accessToken) || IsTokenExpired(accessToken))
                {
                    if (string.IsNullOrEmpty(refreshToken))
                    {
                        Console.WriteLine("-----------------------RefreshToken YOK" );

                        // Refresh token yoksa login sayfasına yönlendir, ReturnUrl ekle
                        //context.Response.Redirect($"/Auth/Login?returnUrl={returnUrl}");
                        context.Response.Redirect($"/Auth/Login?area=&returnUrl={returnUrl}");
                        return;
                    }

                    // Refresh token ile yeni access token al
                    if (!await _refreshTokenService.RefreshTokensAsync())
                    {
                        // Refresh token da geçersizse login sayfasına yönlendir, ReturnUrl ekle
                        context.Response.Redirect($"/Auth/Login?area=&returnUrl={returnUrl}");
                        return;
                    }

                    Console.WriteLine("-----------------------ACCESS -- REFRESH ALINDI KAYDEDILDI");
                    Console.WriteLine("-----------------------" + context.Request.Path);
                    Console.WriteLine("-----------------------" + accessToken1);
                    Console.WriteLine("-----------------------" + _refreshTokenService.GetAccessToken());
                }
            }

            // Orijinal isteği işle
            await _next(context);
        }

        private bool IsTokenExpired(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                // Expiration (exp) claim'ini kontrol et
                return jwtToken.ValidTo < DateTime.UtcNow;
            }
            catch
            {
                // Token geçersizse expired olarak kabul et
                return true;
            }
        }
    }
}