using CourseCenter.WebUI.DTOs.BlogCategoryDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BlogCategoryController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultBlogCategoryDto>>("BlogCategories");
            return View(datas);
        }

        public async Task<IActionResult> DeleteBlogCategory(int id)
        {
            await _client.DeleteAsync($"BlogCategories/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateBlogCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlogCategory(CreateBlogCategoryDto createBlogCategoryDto)
        {
            var validator = new CreateBlogCategoryValidator();
            var result = await validator.ValidateAsync(createBlogCategoryDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(createBlogCategoryDto);
            }

            await _client.PostAsJsonAsync("BlogCategories", createBlogCategoryDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBlogCategory(int id)
        {
            var datas = await _client.GetFromJsonAsync<UpdateBlogCategoryDto>($"BlogCategories/{id}");
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlogCategory(UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            var validator = new UpdateBlogCategoryValidator();
            var result = await validator.ValidateAsync(updateBlogCategoryDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(updateBlogCategoryDto);
            }

            await _client.PutAsJsonAsync("BlogCategories", updateBlogCategoryDto);
            return RedirectToAction(nameof(Index));
        }
    }
}
