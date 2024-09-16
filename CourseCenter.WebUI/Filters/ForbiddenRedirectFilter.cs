using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourseCenter.WebUI.Filters
{
    public class ForbiddenRedirectFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                // Ulaşılmak istenen URL'yi alt
                var returnUrl = context.HttpContext.Request.Path;

                // TempData veya QueryString ile AccessDenied sayfasına yönlendirme yap
                var routeValues = new { ReturnUrl = returnUrl };
                context.Result = new RedirectToActionResult("AccessDenied", "Home", routeValues);
            }
        }
    }
}
