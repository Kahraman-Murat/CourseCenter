using CourseCenter.WebUI.DTOs.CourseCategoryDtos;
using CourseCenter.WebUI.DTOs.CourseDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CourseController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();

        private async Task<List<ResultCourseCategoryDto>> GetCourseCategoriesAsync()
        {
            return await _client.GetFromJsonAsync<List<ResultCourseCategoryDto>>("CourseCategories");
        }

        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultCourseDto>>("Courses");
            return View(datas);
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _client.DeleteAsync($"Courses/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {

            var categories = await GetCourseCategoriesAsync();
            ViewBag.courseCategories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
        {
            var validator = new CreateCourseValidator();
            var result = await validator.ValidateAsync(createCourseDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                var categories = await GetCourseCategoriesAsync();
                ViewBag.courseCategories = new SelectList(categories, "Id", "Name");

                return View(createCourseDto);
            }

            await _client.PostAsJsonAsync("Courses", createCourseDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            var categories = await GetCourseCategoriesAsync();
            ViewBag.courseCategories = new SelectList(categories, "Id", "Name");

            var data = await _client.GetFromJsonAsync<UpdateCourseDto>($"Courses/{id}");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto updateCourseDto)
        {
            var validator = new UpdateCourseValidator();
            var result = await validator.ValidateAsync(updateCourseDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                var categories = await GetCourseCategoriesAsync();
                ViewBag.courseCategories = new SelectList(categories, "Id", "Name");

                return View(updateCourseDto);
            }

            await _client.PutAsJsonAsync("Courses", updateCourseDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SetCourseDisplayStatus(int id)
        {
            await _client.GetAsync($"Courses/SetCourseDisplayStatus/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
