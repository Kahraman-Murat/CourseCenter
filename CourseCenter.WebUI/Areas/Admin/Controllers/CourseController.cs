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
    public class CourseController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        private async Task<List<ResultCourseCategoryDto>> GetCourseCategoriesAsync() =>
            await _httpClientService.SendRequestAsync<string, List<ResultCourseCategoryDto>>(HttpMethod.Get, "CourseCategories", default);

        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultCourseDto>>(HttpMethod.Get, "Courses", default));

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
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                var categories = await GetCourseCategoriesAsync();
                ViewBag.courseCategories = new SelectList(categories, "Id", "Name");

                return View(createCourseDto);
            }

            await _httpClientService.SendRequestAsync<CreateCourseDto, string>(HttpMethod.Post, "Courses", createCourseDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            var categories = await GetCourseCategoriesAsync();
            ViewBag.courseCategories = new SelectList(categories, "Id", "Name");

            UpdateCourseDto data = await _httpClientService.SendRequestAsync<string, UpdateCourseDto>(HttpMethod.Get, $"Courses/{id}", default);

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
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                var categories = await GetCourseCategoriesAsync();
                ViewBag.courseCategories = new SelectList(categories, "Id", "Name");

                return View(updateCourseDto);
            }

            await _httpClientService.SendRequestAsync<UpdateCourseDto, string>(HttpMethod.Put, "Courses", updateCourseDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"Courses/{id}", default);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SetCourseDisplayStatus(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Get, $"Courses/SetCourseDisplayStatus/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}
