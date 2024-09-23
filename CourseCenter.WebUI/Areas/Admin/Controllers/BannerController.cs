using CourseCenter.WebUI.DTOs.BannerDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BannerController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultBannerDto>>(HttpMethod.Get, "Banners", default));

        [HttpGet]
        public async Task<IActionResult> CreateBanner() => View();

        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerDto createBannerDto)
        {
            var validator = new CreateBannerValidator();
            var result = await validator.ValidateAsync(createBannerDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                
                return View(createBannerDto);
            }
            await _httpClientService.SendRequestAsync<CreateBannerDto, string>(HttpMethod.Post, "Banners", createBannerDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBanner(int id) =>
            View(await _httpClientService.SendRequestAsync<string, UpdateBannerDto>(HttpMethod.Get, $"Banners/{id}", default));

        [HttpPost]
        public async Task<IActionResult> UpdateBanner(UpdateBannerDto updateBannerDto)
        {
            var validator = new UpdateBannerValidator();
            var result = await validator.ValidateAsync(updateBannerDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(updateBannerDto);
            }

            await _httpClientService.SendRequestAsync<UpdateBannerDto, string>(HttpMethod.Put, "Banners", updateBannerDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteBanner(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"Banners/{id}", default);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
