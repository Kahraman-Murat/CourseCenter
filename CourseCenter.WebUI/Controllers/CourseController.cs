using CourseCenter.WebUI.DTOs.CourseCategoryDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Controllers
{
    public class CourseController(IHttpClientService _httpClientService) : Controller
    {

        [Route("Course/Index/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.Category = id;

            List<ResultCourseCategoryDto> response = await _httpClientService.SendRequestAsync<string, List<ResultCourseCategoryDto>>(HttpMethod.Get, $"CourseCategories/GetCategoriesWithCoursesUndTeacherById/{id}", default);

            return View(response); 
        }
    }
}
