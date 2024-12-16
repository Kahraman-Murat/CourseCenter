using CourseCenter.WebUI.DTOs.CourseCategoryDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class CourseCategoryController(IHttpClientService _httpClientService) : Controller
    {

        List<(string Text, string Icon)> courseCategoryIconsList = new() 
            {
                ("Icon - Bilgisayar", "flaticon-computing"),
                ("Icon - Egitim", "flaticon-education"),
                ("Icon - Isletme","flaticon-business"),
                ("Icon - Danisman","flaticon-communications-1"),
                ("Icon - Mezuniyet","flaticon-graduated"),
                ("Icon - Kontrol Listei","flaticon-tools-and-utensils"),
                ("Icon - Iletisim","flaticon-communications"),
                ("Icon - Tasarim Dizayn","flaticon-web-design"),
            };

        [HttpGet]
        public async Task<IActionResult> Index() => View(await _httpClientService.SendRequestAsync<string, List<ResultCourseCategoryDto>>(HttpMethod.Get, "CourseCategories", default));

        [HttpGet]
        public async Task<IActionResult> CreateCourseCategory()
        {            
            ViewBag.DropdownItems = courseCategoryIconsList;

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
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                ViewBag.DropdownItems = courseCategoryIconsList;

                return View(createCourseCategoryDto);
            }

            await _httpClientService.SendRequestAsync<CreateCourseCategoryDto, string>(HttpMethod.Post, "CourseCategories", createCourseCategoryDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCourseCategory(int id)
        {
            ViewBag.DropdownItems = courseCategoryIconsList;

            UpdateCourseCategoryDto updateCourseCategoryDto = await _httpClientService.SendRequestAsync<string, UpdateCourseCategoryDto>(HttpMethod.Get, $"CourseCategories/{id}", default);

            return View(updateCourseCategoryDto);
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
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                ViewBag.DropdownItems = courseCategoryIconsList;

                return View(updateCourseCategoryDto);
            }

            await _httpClientService.SendRequestAsync<UpdateCourseCategoryDto, string>(HttpMethod.Put, "CourseCategories", updateCourseCategoryDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCourseCategory(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"CourseCategories/{id}", default);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SetCourseCategoryDisplayStatus(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Get, $"CourseCategories/SetCourseCategoryDisplayStatus/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}
