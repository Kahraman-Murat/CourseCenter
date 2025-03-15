
using CourseCenter.WebUI.DTOs.ContactDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.UILayout
{
    public class _UILayoutFooterSubscribeComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
