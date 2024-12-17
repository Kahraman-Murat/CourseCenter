using CourseCenter.WebUI.DTOs.AuthDtos;
using CourseCenter.WebUI.DTOs.CourseCategoryDtos;
using CourseCenter.WebUI.DTOs.CourseDtos;
using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.Filters;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace CourseCenter.WebUI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [TypeFilter(typeof(JwtUserFromTokenFilter))]
    public class CourseController(IHttpClientService _httpClientService, IRefreshTokenService _refreshTokenService) : Controller
    {
        [HttpGet]
        private async Task<List<ResultCourseCategoryDto>> GetCourseCategoriesAsync() => await _httpClientService.SendRequestAsync<string, List<ResultCourseCategoryDto>>(HttpMethod.Get, "CourseCategories", default);

        [HttpGet]
        private async Task<List<ResultUserDto>> GetUsersInRoleAsync(string roleName) =>
            await _httpClientService.SendRequestAsync<string, List<ResultUserDto>>(HttpMethod.Get, $"Users/GetUsersInRole/{roleName}", default);

        public async Task<IActionResult> Index()
        {
            // Kullanıcıyı HttpContext.Items üzerinden al
            string userId = HttpContext.Items["UserId"].ToString();

            List<ResultCourseDto> courses = await _httpClientService.SendRequestAsync<string, List<ResultCourseDto>>(HttpMethod.Get, $"Courses/GetCourseByTeacherId/{userId}", default);

            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            var categories = await GetCourseCategoriesAsync();
            ViewBag.courseCategories = new SelectList(categories, "Id", "Name");

            var teachers = await GetUsersInRoleAsync("Teacher");
            ViewBag.courseTeachers = new SelectList(teachers, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto createCourseDto)
        {
            createCourseDto.IsShown = false; // Admin should be able to set
            var validator = new CreateCourseValidator();
            var result = await validator.ValidateAsync(createCourseDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                var categories = await GetCourseCategoriesAsync();
                ViewBag.courseCategories = new SelectList(categories, "Id", "Name");

                var teachers = await GetUsersInRoleAsync("Teacher");
                ViewBag.courseTeachers = new SelectList(teachers, "Id", "FullName");

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

            var teachers = await GetUsersInRoleAsync("Teacher");
            ViewBag.courseTeachers = new SelectList(teachers, "Id", "FullName");

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

                var teachers = await GetUsersInRoleAsync("Teacher");
                ViewBag.courseTeachers = new SelectList(teachers, "Id", "FullName");

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
    }
}
