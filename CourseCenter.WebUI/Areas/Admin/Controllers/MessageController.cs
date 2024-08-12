using CourseCenter.WebUI.DTOs.MessageDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class MessageController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultMessageDto>>("Messages");
            return View(datas);
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _client.DeleteAsync($"Messages/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> MessageDetail(int id)
        {
            var data = await _client.GetFromJsonAsync<ResultMessageDto>($"Messages/{id}");
            return View(data);
        }
                
    }
}