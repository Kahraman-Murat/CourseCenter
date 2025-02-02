using CourseCenter.WebUI.DTOs.BlogDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Blog
{
    public class _BlogListComponent(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultBlogDto>>(HttpMethod.Get, "blogs", default));
    }
}
