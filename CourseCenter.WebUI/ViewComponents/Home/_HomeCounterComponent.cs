using CourseCenter.WebUI.DTOs.BannerDtos;
using CourseCenter.WebUI.DTOs.BlogDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Home
{
    public class _HomeCounterComponent(IHttpClientService _httpClientService) : ViewComponent
    {
        //public async Task<IViewComponentResult> InvokeAsync() => 
        //    View(await _httpClientService.SendRequestAsync<string, List<ResultBannerDto>>(HttpMethod.Get, "banners", default));
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            ViewBag.courseCategoryCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "courseCategories/GetCategoryCount", default);
            ViewBag.courseCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "courses/GetCourseCount", default);
            ViewBag.blogCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "blogs/GetBlogCount", default);
            ViewBag.teacherCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "users/GetUserCountInRoles/Teacher", default);
            return View();
        }
    }
}
