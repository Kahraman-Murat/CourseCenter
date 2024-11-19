
using CourseCenter.WebUI.DTOs.ContactDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.UILayout
{
    public class _UILayoutHeaderContactInfoComponent(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultContactDto>>(HttpMethod.Get, "contacts", default));
    }
}
