using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Web;

namespace CourseCenter.WebUI.Middlewares
{
    public class JwtAreaRoleCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRefreshTokenService _refreshTokenService;

        public JwtAreaRoleCheckMiddleware(RequestDelegate next, IRefreshTokenService refreshTokenService)
        {
            _next = next;
            _refreshTokenService = refreshTokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // JwtAreaRoleCheckMiddleware'in aktif olacagi area lari belirle
            var areas = new[]
            {
                "Admin",
                "Teacher",
                "Student",
            };

            // Area bilgisini URL'den al
            var returnUrl = context.Request.Path.ToString();
            var area = ExtractAreaFromPath(returnUrl);


            if (!string.IsNullOrEmpty(area) && areas.Any(item => item == area))
            {
                // Kullanıcı token'ından rolleri al 
                var userRoles = _refreshTokenService.GetUserRolesFromToken(_refreshTokenService.GetAccessToken());

                // Kullanıcı rollerini area ile karşılaştır
                if (userRoles == null || !userRoles.Contains(area))
                {
                    // Eğer area ile uyumlu bir rol yoksa, Login'e yönlendir
                    //var returnUrl = path;
                    context.Response.Redirect($"/Auth/Login?area=&returnUrl={returnUrl}");
                    return;
                }
            }

            await _next(context);
        }

        // Path'den Area'yı çıkar
        private string ExtractAreaFromPath(string path)
        {
            var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (segments.Length > 1)
                return segments[0]; // "Admin" veya "Teacher" gibi area bilgisi path'in ilk segmentinde yer alır

            return null;
        }
    }
}