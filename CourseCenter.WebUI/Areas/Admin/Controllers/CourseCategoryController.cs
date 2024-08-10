using CourseCenter.WebUI.DTOs.CourseCategoryDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CourseCategoryController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultCourseCategoryDto>>("CourseCategories");
            return View(datas);
        }

        public async Task<IActionResult> DeleteCourseCategory(int id)
        {
            await _client.DeleteAsync($"CourseCategories/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateCourseCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourseCategory(CreateCourseCategoryDto createCourseCategoryDto)
        {
            var validator = new CreateCourseCategoryValidator();
            var result = await validator.ValidateAsync(createCourseCategoryDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(createCourseCategoryDto);
            }

            await _client.PostAsJsonAsync("CourseCategories", createCourseCategoryDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCourseCategory(int id)
        {
            var datas = await _client.GetFromJsonAsync<UpdateCourseCategoryDto>($"CourseCategories/{id}");
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourseCategory(UpdateCourseCategoryDto updateCourseCategoryDto)
        {
            var validator = new UpdateCourseCategoryValidator();
            var result = await validator.ValidateAsync(updateCourseCategoryDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(updateCourseCategoryDto);
            }

            await _client.PutAsJsonAsync("CourseCategories", updateCourseCategoryDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SetCourseCategoryDisplayStatus(int id)
        {
            await _client.GetAsync($"CourseCategories/SetCourseCategoryDisplayStatus/{id}");
            return RedirectToAction(nameof(Index));
        }

    }
}
