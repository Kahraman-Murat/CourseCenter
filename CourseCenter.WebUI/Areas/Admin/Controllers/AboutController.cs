using CourseCenter.WebUI.DTOs.AboutDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Json;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class AboutController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultAboutDto>>("abouts");
            return View(datas);
        }

        public async Task<IActionResult> DeleteAbout(int id)
        {
            await _client.DeleteAsync($"abouts/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateAbout()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            var validator = new CreateAboutValidator();
            var result = await validator.ValidateAsync(createAboutDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(createAboutDto);
            }

            await _client.PostAsJsonAsync("abouts", createAboutDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var datas = await _client.GetFromJsonAsync<UpdateAboutDto>($"abouts/{id}");
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            var validator = new UpdateAboutValidator();
            var result = await validator.ValidateAsync(updateAboutDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(updateAboutDto);
            }

            await _client.PutAsJsonAsync("abouts", updateAboutDto);
            return RedirectToAction(nameof(Index));
        }
    }
}
