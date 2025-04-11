using CourseCenter.WebUI.DTOs.CourseCategoryDtos;
using CourseCenter.WebUI.DTOs.CourseDtos;
using CourseCenter.WebUI.DTOs.CourseRegisterDtos;
using CourseCenter.WebUI.DTOs.CourseVideoDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseCenter.WebUI.Areas.Student.Controllers
{
    [Area("Student")]
    public class CourseRegisterController(IHttpClientService _httpClientService) : Controller
    {
        private async Task<List<ResultCourseDto>> GetCoursesAsync() => await _httpClientService.SendRequestAsync<string, List<ResultCourseDto>>(HttpMethod.Get, "Courses", default);

        public async Task<IActionResult> Index()
        {
            List<ResultCourseRegisterDto> courses = await _httpClientService.SendRequestAsync<string, List<ResultCourseRegisterDto>>(HttpMethod.Get, $"CourseRegisters/GetCourseByStudentId", default);

            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var courses = await GetCoursesAsync();
            ViewBag.courses = new SelectList(courses, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateCourseRegisterDto createCourseRegisterDto)
        {
            createCourseRegisterDto.StudentId = 0; //Set in Backend 

            var validator = new CreateCourseRegisterValidator();
            var result = await validator.ValidateAsync(createCourseRegisterDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                var courses = await GetCoursesAsync();
                ViewBag.courses = new SelectList(courses, "Id", "Name");

                return View(createCourseRegisterDto);
            }

            await _httpClientService.SendRequestAsync<CreateCourseRegisterDto, string>(HttpMethod.Post, "CourseRegisters", createCourseRegisterDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var courses = await GetCoursesAsync();
            ViewBag.courses = new SelectList(courses, "Id", "Name");

            UpdateCourseRegisterDto data = await _httpClientService.SendRequestAsync<string, UpdateCourseRegisterDto>(HttpMethod.Get, $"CourseRegisters/{id}", default);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCourseRegisterDto updateCourseRegisterDto)
        {
            var validator = new UpdateCourseRegisterValidator();
            var result = await validator.ValidateAsync(updateCourseRegisterDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                var courses = await GetCoursesAsync();
                ViewBag.courses = new SelectList(courses, "Id", "Name");

                return View(updateCourseRegisterDto);
            }

            await _httpClientService.SendRequestAsync<UpdateCourseRegisterDto, string>(HttpMethod.Put, "CourseRegisters", updateCourseRegisterDto);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Delete(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"CourseRegisters/{id}", default);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CourseVideos(int id)
        {

            List<ResultCourseVideoDto> videos = await _httpClientService.SendRequestAsync<string, List<ResultCourseVideoDto>>(HttpMethod.Get, $"CourseVideos/GetVideoListByCourseId/{id}", default);

            ViewBag.CourseId = id;
            ViewBag.CourseName = videos.FirstOrDefault()?.Course.Name;

            return View(videos);
        }
    }
}
