using CourseCenter.WebUI.DTOs.BannerDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Home
{
    public class _HomeBannerComponent : ViewComponent
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultBannerDto>>("banners");
            return View(datas);
        }
    }
}
