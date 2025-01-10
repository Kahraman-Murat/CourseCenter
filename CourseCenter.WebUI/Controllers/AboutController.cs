using CourseCenter.WebUI.DTOs.AboutDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Controllers
{
    public class AboutController(IHttpClientService _httpClientService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<ResultAboutDto> response = await _httpClientService.SendRequestAsync<string, List<ResultAboutDto>>(HttpMethod.Get, $"Abouts", default);

            return View(response);
        }
    }
}
