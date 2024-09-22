using CourseCenter.WebUI.DTOs.AboutDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultAboutDto>>(HttpMethod.Get, "abouts", default));

        [HttpGet]
        public async Task<IActionResult> CreateAbout() => View();

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var validator = new CreateAboutValidator();
            var result = await validator.ValidateAsync(createAboutDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);                

                return View(createAboutDto);
            }

            await _httpClientService.SendRequestAsync<CreateAboutDto, string>(HttpMethod.Post, "abouts", createAboutDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id) => 
            View(await _httpClientService.SendRequestAsync<string, UpdateAboutDto>(HttpMethod.Get, $"abouts/{id}", default));

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var validator = new UpdateAboutValidator();
            var result = await validator.ValidateAsync(updateAboutDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(updateAboutDto);
            }

            await _httpClientService.SendRequestAsync<UpdateAboutDto, string>(HttpMethod.Put,  "abouts", updateAboutDto);

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> DeleteAbout(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"abouts/{id}", default);
                        
            return RedirectToAction(nameof(Index));
        }
    }
}
