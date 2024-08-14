using CourseCenter.WebUI.DTOs.SubscriberDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SubscriberController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultSubscriberDto>>("Subscribers");
            return View(datas);
        }

        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            await _client.DeleteAsync($"Subscribers/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateSubscriber()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(CreateSubscriberDto createSubscriberDto)
        {
            var validator = new CreateSubscriberValidator();
            var result = await validator.ValidateAsync(createSubscriberDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(createSubscriberDto);
            }

            await _client.PostAsJsonAsync("Subscribers", createSubscriberDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubscriber(int id)
        {
            var datas = await _client.GetFromJsonAsync<UpdateSubscriberDto>($"Subscribers/{id}");
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubscriber(UpdateSubscriberDto updateSubscriberDto)
        {
            var validator = new UpdateSubscriberValidator();
            var result = await validator.ValidateAsync(updateSubscriberDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(updateSubscriberDto);
            }

            await _client.PutAsJsonAsync("Subscribers", updateSubscriberDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SetSubscriberStatus(int id)
        {
            await _client.GetAsync($"Subscribers/SetSubscriberStatus/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}