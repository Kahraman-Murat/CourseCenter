using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using CourseCenter.WebUI.DTOs.AuthDtos;
using System.Security.Claims;
using CourseCenter.WebUI.Helpers;

namespace CourseCenter.WebUI.Filters
{
    public class JwtUserFromTokenFilter(IRefreshTokenService _refreshTokenService) : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpContext = context.HttpContext;

            // Ulaşılmak istenen URL'yi al
            var returnUrl = httpContext.Request.Path;            
            var routeValues = new { area = "", ReturnUrl = returnUrl };


            // Access token'ı cookie'de yoksa login sayfasına yönlendir
            var token = _refreshTokenService.GetAccessToken();
            if (string.IsNullOrEmpty(token))
            {
                //return RedirectToAction("Login", "Account", new { area = "", returnUrl = "/Dashboard", userId = 123 });
                context.Result = new RedirectToActionResult("Login", "Auth", routeValues);
                return;
            }

            try
            {
                // Token'ı çözümleyin
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                //  JWTValidationMiddleware e ragmen Token süresi geçmişse login sayfasına yönlendir
                if (jwtToken.ValidTo < DateTime.UtcNow)
                    context.Result = new RedirectToActionResult("Login", "Auth", routeValues);                

                // Kullanıcı bilgilerini HttpContext'e ekle
                httpContext.Items["UserId"] = jwtToken.Payload[ClaimTypes.NameIdentifier]?.ToString();
                httpContext.Items["UserFullName"] = jwtToken.Payload[JwtRegisteredClaimNames.Name]?.ToString();
                httpContext.Items["UserEmail"] = jwtToken.Payload[JwtRegisteredClaimNames.Email]?.ToString();

                Console.WriteLine("-----------------------FILTER USER DATA");
                Console.WriteLine("-----------------------" + httpContext.Items["UserId"].ToString());
                Console.WriteLine("-----------------------" + httpContext.Items["UserFullName"].ToString());
                Console.WriteLine("-----------------------" + httpContext.Items["UserEmail"].ToString());
            }
            catch (Exception)
            {
                // Token geçerli değilse login sayfasına yönlendir
                context.Result = new RedirectToActionResult("Login", "Auth", routeValues);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Bu metot action sonrası işlemler için kullanılabilir, burada boş bırakıyoruz.
        }
    }
}