using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Home
{
    public class _HomeTeacherComponent(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultUserSocialMediasDto>>(HttpMethod.Get, "users/GetLast4Teachers", default));
    }
}
