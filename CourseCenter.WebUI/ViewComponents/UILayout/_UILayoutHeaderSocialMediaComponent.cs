using CourseCenter.WebUI.DTOs.SocialMediaDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.UILayout
{
    public class _UILayoutHeaderSocialMediaComponent(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultSocialMediaDto>>(HttpMethod.Get, "SocialMedias", default));
    }
}
