using CourseCenter.WebUI.DTOs.AboutDtos;
using CourseCenter.WebUI.DTOs.BlogDtos;
using CourseCenter.WebUI.DTOs.CourseDtos;
using CourseCenter.WebUI.DTOs.CourseVideoDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class DashboardController(IHttpClientService _httpClientService) : Controller
    {

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.blogCategoryCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "blogCategories/GetBlogCategoryCount", default);
            var blogs = await _httpClientService.SendRequestAsync<string, List<ResultBlogDto>>(HttpMethod.Get, "blogs/GetBlogsByWriterId/0", default);
            ViewBag.blogCount = blogs.Count;

            ViewBag.courseCategoryCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "courseCategories/GetCategoryCount", default);
            var courses = await _httpClientService.SendRequestAsync<string, List<ResultCourseDto>>(HttpMethod.Get, "courses/GetCourseByTeacherId/0", default);
            ViewBag.courseCount = courses.Count;

            var videos = await _httpClientService.SendRequestAsync<string, List<ResultCourseVideoDto>>(HttpMethod.Get, "courseVideos/GetVideoListForTeacher", default);
            ViewBag.videoCount = videos.Count;

            ViewBag.messageCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "messages/GetmessageCount", default);

            return View();
        }
    }
}
