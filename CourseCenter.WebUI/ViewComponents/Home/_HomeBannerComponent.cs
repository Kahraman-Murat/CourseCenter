using CourseCenter.WebUI.DTOs.BannerDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Home
{
    public class _HomeBannerComponent(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() => 
            View(await _httpClientService.SendRequestAsync<string, List<ResultBannerDto>>(HttpMethod.Get, "banners", default));
    }
}
