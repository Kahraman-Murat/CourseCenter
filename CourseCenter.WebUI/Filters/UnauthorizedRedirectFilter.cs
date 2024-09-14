using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CourseCenter.WebUI.Filters
{
    public class UnauthorizedRedirectFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
        
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Action çalıştıktan sonra, Unauthorized (401) durum kodunu kontrol et
            if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                // Ulaşılmak istenen URL'yi alt
                var returnUrl = context.HttpContext.Request.Path;

                // TempData veya QueryString ile Login sayfasına yönlendirme yap
                var routeValues = new { ReturnUrl = returnUrl };
                context.Result = new RedirectToActionResult("Login", "Auth", routeValues);

            }
        }
    }
}
