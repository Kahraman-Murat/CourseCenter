using CourseCenter.WebUI.DTOs.SocialMediaDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class SocialMediaController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultSocialMediaDto>>(HttpMethod.Get, "SocialMedias", default));

        [HttpGet]
        public async Task<IActionResult> CreateSocialMedia() => View();

        [HttpPost]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            var validator = new CreateSocialMediaValidator();
            var result = await validator.ValidateAsync(createSocialMediaDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(createSocialMediaDto);
            }

            await _httpClientService.SendRequestAsync<CreateSocialMediaDto, string>(HttpMethod.Post, "SocialMedias", createSocialMediaDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSocialMedia(int id) =>
            View(await _httpClientService.SendRequestAsync<string, UpdateSocialMediaDto>(HttpMethod.Get, $"SocialMedias/{id}", default));

        [HttpPost]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var validator = new UpdateSocialMediaValidator();
            var result = await validator.ValidateAsync(updateSocialMediaDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(updateSocialMediaDto);
            }

            await _httpClientService.SendRequestAsync<UpdateSocialMediaDto, string>(HttpMethod.Put, "SocialMedias", updateSocialMediaDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"SocialMedias/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}
