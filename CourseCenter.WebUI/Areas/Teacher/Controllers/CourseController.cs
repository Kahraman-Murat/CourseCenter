using CourseCenter.WebUI.DTOs.AuthDtos;
using CourseCenter.WebUI.DTOs.CourseDtos;
using CourseCenter.WebUI.Filters;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;

namespace CourseCenter.WebUI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [TypeFilter(typeof(JwtUserFromTokenFilter))]
    public class CourseController(IHttpClientService _httpClientService, IRefreshTokenService _refreshTokenService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            // Kullanıcıyı HttpContext.Items üzerinden al
            string userId = HttpContext.Items["UserId"].ToString();

            List<ResultCourseDto> courses = await _httpClientService.SendRequestAsync<string, List<ResultCourseDto>>(HttpMethod.Get, $"Courses/GetCourseByTeacherId/{userId}", default);

            return View(courses);
        }
    }
}
