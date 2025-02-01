using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Blog
{
    public class _BlogSubscribeComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}