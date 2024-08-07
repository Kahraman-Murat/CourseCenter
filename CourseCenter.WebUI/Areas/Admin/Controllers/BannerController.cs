using CourseCenter.WebUI.DTOs.BannerDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BannerController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultBannerDto>>("Banners");
            return View(datas);
        }

        public async Task<IActionResult> DeleteBanner(int id)
        {
            await _client.DeleteAsync($"Banners/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateBanner()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBanner(CreateBannerDto createBannerDto)
        {
            var validator = new CreateBannerValidator();
            var result = await validator.ValidateAsync(createBannerDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
                return View(result);
            }

            await _client.PostAsJsonAsync("Banners", createBannerDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBanner(int id)
        {
            var datas = await _client.GetFromJsonAsync<UpdateBannerDto>($"Banners/{id}");
            return View(datas);
        }

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
                return View(result);
            }

            await _client.PutAsJsonAsync("Banners", updateBannerDto);
            return RedirectToAction(nameof(Index));
        }
    }
}
