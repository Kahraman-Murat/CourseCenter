using AutoMapper;
using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Controllers
{
    public class TeachersController(IHttpClientService _httpClientService) : Controller
    {
        [Route("Teachers/Index/{page}")]
        public async Task<IActionResult> Index(int page)
        {
            List<ResultUserSocialMediasDto> response = await _httpClientService.SendRequestAsync<string, List<ResultUserSocialMediasDto>>(HttpMethod.Get, $"Users/GetTeachersWithSocialMedia/{page}", default);

            return View(response);
        }
    }
}
