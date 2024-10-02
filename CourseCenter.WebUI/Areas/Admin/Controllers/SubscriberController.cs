using CourseCenter.WebUI.DTOs.AboutDtos;
using CourseCenter.WebUI.DTOs.SubscriberDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SubscriberController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultSubscriberDto>>(HttpMethod.Get, "Subscribers", default));

        [HttpGet]
        public async Task<IActionResult> CreateSubscriber() => View();

        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(CreateSubscriberDto createSubscriberDto)
        {
            var validator = new CreateSubscriberValidator();
            var result = await validator.ValidateAsync(createSubscriberDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(createSubscriberDto);
            }

            await _httpClientService.SendRequestAsync<CreateSubscriberDto, string>(HttpMethod.Post, "Subscribers", createSubscriberDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSubscriber(int id) =>
            View(await _httpClientService.SendRequestAsync<string, UpdateSubscriberDto>(HttpMethod.Get, $"Subscribers/{id}", default));

        [HttpPost]
        public async Task<IActionResult> UpdateSubscriber(UpdateSubscriberDto updateSubscriberDto)
        {
            var validator = new UpdateSubscriberValidator();
            var result = await validator.ValidateAsync(updateSubscriberDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(updateSubscriberDto);
            }

            await _httpClientService.SendRequestAsync<UpdateSubscriberDto, string>(HttpMethod.Put, "Subscribers", updateSubscriberDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"Subscribers/{id}", default);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SetSubscriberStatus(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Get, $"Subscribers/SetSubscriberStatus/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}