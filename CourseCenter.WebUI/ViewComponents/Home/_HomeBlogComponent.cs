
using CourseCenter.WebUI.DTOs.BlogDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Home
{
    public class _HomeBlogComponent(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultBlogDto>>(HttpMethod.Get, "blogs/GetLast4Blogs", default));
    }
}