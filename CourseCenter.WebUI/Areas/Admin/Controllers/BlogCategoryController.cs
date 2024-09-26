using CourseCenter.WebUI.DTOs.BlogCategoryDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BlogCategoryController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultBlogCategoryDto>>(HttpMethod.Get, "BlogCategories", default));

        [HttpGet]
        public async Task<IActionResult> CreateBlogCategory() => View();

        [HttpPost]
        public async Task<IActionResult> CreateBlogCategory(CreateBlogCategoryDto createBlogCategoryDto)
        {
            var validator = new CreateBlogCategoryValidator();
            var result = await validator.ValidateAsync(createBlogCategoryDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(createBlogCategoryDto);
            }

            await _httpClientService.SendRequestAsync<CreateBlogCategoryDto, string>(HttpMethod.Post, "BlogCategories", createBlogCategoryDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBlogCategory(int id) =>
            View(await _httpClientService.SendRequestAsync<string, UpdateBlogCategoryDto>(HttpMethod.Get, $"BlogCategories/{id}", default));

        [HttpPost]
        public async Task<IActionResult> UpdateBlogCategory(UpdateBlogCategoryDto updateBlogCategoryDto)
        {
            var validator = new UpdateBlogCategoryValidator();
            var result = await validator.ValidateAsync(updateBlogCategoryDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(updateBlogCategoryDto);
            }

            await _httpClientService.SendRequestAsync<UpdateBlogCategoryDto, string>(HttpMethod.Put, "BlogCategories", updateBlogCategoryDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteBlogCategory(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"BlogCategories/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}
