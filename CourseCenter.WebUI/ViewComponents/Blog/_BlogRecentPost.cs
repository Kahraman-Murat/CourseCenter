using CourseCenter.WebUI.DTOs.BlogDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Blog
{
    public class _BlogRecentPost(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ResultBlogDto> blogs = await _httpClientService.SendRequestAsync<string, List<ResultBlogDto>>(HttpMethod.Get, "blogs/GetLast4Blogs", default);

            return View(blogs);
        }
    }
}
