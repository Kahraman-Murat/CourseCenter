using CourseCenter.WebUI.DTOs.CourseDtos;
using CourseCenter.WebUI.DTOs.CourseVideoDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CourseVideoController(IHttpClientService _httpClientService, IRefreshTokenService _refreshTokenService) : Controller
    {
        private async Task<List<ResultCourseDto>> GetCoursesAsync() => await _httpClientService.SendRequestAsync<string, List<ResultCourseDto>>(HttpMethod.Get, "Courses", default);

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {

            List<ResultCourseVideoDto> videos = await _httpClientService.SendRequestAsync<string, List<ResultCourseVideoDto>>(HttpMethod.Get, $"CourseVideos/GetVideoListByCourseId/{id}", default);

            return View(videos);
        }

        [HttpGet]
        public async Task<IActionResult> AddVideo(int id)
        {
            var courses = await GetCoursesAsync();
            ViewBag.courses = new SelectList(courses, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVideo(CreateCourseVideoDto createCourseVideoDto)
        {

            var validator = new CreateCourseVideoValidator();
            var result = await validator.ValidateAsync(createCourseVideoDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(createCourseVideoDto);
            }

            await _httpClientService.SendRequestAsync<CreateCourseVideoDto, string>(HttpMethod.Post, "CourseVideos", createCourseVideoDto);

            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> UpdateVideo(int id)
        {
            var courses = await GetCoursesAsync();
            ViewBag.courses = new SelectList(courses, "Id", "Name");

            List<ResultCourseVideoDto> videos = await _httpClientService.SendRequestAsync<string, List<ResultCourseVideoDto>>(HttpMethod.Get, $"CourseVideos/GetVideoListByCourseId/{id}", default);

            UpdateCourseVideoDto data = await _httpClientService.SendRequestAsync<string, UpdateCourseVideoDto>(HttpMethod.Get, $"CourseVideos/{id}", default);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVideo(UpdateCourseVideoDto updateCourseVideoDto)
        {
            var validator = new UpdateCourseVideoValıdator();
            var result = await validator.ValidateAsync(updateCourseVideoDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(updateCourseVideoDto);
            }

            await _httpClientService.SendRequestAsync<UpdateCourseVideoDto, string>(HttpMethod.Put, "CourseVideos", updateCourseVideoDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteVideo(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"CourseVideos/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}
