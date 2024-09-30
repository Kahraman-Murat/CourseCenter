using CourseCenter.WebUI.DTOs.MessageDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class MessageController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]        
        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultMessageDto>>(HttpMethod.Get, "Messages", default));

        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"Messages/{id}", default);

            return RedirectToAction(nameof(Index));
        }
                
        public async Task<IActionResult> MessageDetail(int id) =>
            View(await _httpClientService.SendRequestAsync<string, ResultMessageDto>(HttpMethod.Get, $"Messages/{id}", default));
                
    }
}