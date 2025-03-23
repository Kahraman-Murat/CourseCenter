using CourseCenter.WebUI.DTOs.ContactDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Contact
{
    public class _ContactInfoComponent(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = await _httpClientService.SendRequestAsync<string, List<ResultContactDto>>(HttpMethod.Get, "Contacts", default);
            return View(datas);
        }
    }
}
