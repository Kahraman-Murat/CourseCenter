using CourseCenter.WebUI.DTOs.AboutDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class DashboardController(IHttpClientService _httpClientService) : Controller
    {
        public class userRoleStatus
        {
            public string Role { get; set; }
            public int Count { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.blogCategoryCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "blogCategories/GetBlogCategoryCount", default);
            ViewBag.blogCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "blogs/GetBlogCount", default);

            ViewBag.courseCategoryCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "courseCategories/GetCategoryCount", default);
            ViewBag.courseCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "courses/GetCourseCount", default);

            ViewBag.videoCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "courseVideos/GetVideoCount", default);

            ViewBag.messageCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "messages/GetmessageCount", default);



            ViewBag.subscriberCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "subscribers/GetSubscriberCount", default);

            ViewBag.testimonialCount = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "testimonials/GetTestimonialCount", default);



            List<string> allroles = await _httpClientService.SendRequestAsync<string, List<string>>(HttpMethod.Get, "Roles/GetDefinedRolesInAssembly", default);

            List<userRoleStatus> roleStatus = new();
            int countSum=0;

            foreach (var role in allroles)
            {
                int count = await _httpClientService.SendRequestAsync<string, int>(HttpMethod.Get, "users/GetUserCountInRoles/" + role, default);
                userRoleStatus newStat = new()
                {
                    Role = role,
                    Count = count
                };
                countSum += count;
                roleStatus.Add(newStat);
            }
            ViewBag.userRoleStatus= roleStatus;
            ViewBag.countSum = countSum;


            var datas = await _httpClientService.SendRequestAsync<string, List<ResultAboutDto>>(HttpMethod.Get, "abouts", default);
            return View(datas);
        }
    }
}