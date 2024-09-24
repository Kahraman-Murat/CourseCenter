using CourseCenter.WebUI.DTOs.BlogCategoryDtos;
using CourseCenter.WebUI.DTOs.BlogDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BlogController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        private async Task<List<ResultBlogCategoryDto>> GetBlogCategoriesAsync() =>
            await _httpClientService.SendRequestAsync<string, List<ResultBlogCategoryDto>>(HttpMethod.Get, "BlogCategories", default);

        public async Task<IActionResult> Index() => 
            View(await _httpClientService.SendRequestAsync<string, List<ResultBlogDto>>(HttpMethod.Get, "Blogs", default));

        [HttpGet]
        public async Task<IActionResult> CreateBlog()
        {            
            var categories = await GetBlogCategoriesAsync();
            ViewBag.blogCategories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogDto createBlogDto)
        {
            var validator = new CreateBlogValidator();
            var result = await validator.ValidateAsync(createBlogDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                                
                var categories = await GetBlogCategoriesAsync();
                ViewBag.blogCategories = new SelectList(categories, "Id", "Name");
                                
                return View(createBlogDto);
            }

            await _httpClientService.SendRequestAsync<CreateBlogDto, string>(HttpMethod.Post, "Blogs", createBlogDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBlog(int id)
        {
            var categories = await GetBlogCategoriesAsync();
            ViewBag.blogCategories = new SelectList(categories, "Id", "Name");

            UpdateBlogDto data = await _httpClientService.SendRequestAsync<string, UpdateBlogDto>(HttpMethod.Get, $"Blogs/{id}", default);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlog(UpdateBlogDto updateBlogDto)
        {
            var validator = new UpdateBlogValidator();
            var result = await validator.ValidateAsync(updateBlogDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                var categories = await GetBlogCategoriesAsync();
                ViewBag.blogCategories = new SelectList(categories, "Id", "Name");

                return View(updateBlogDto);
            }

            await _httpClientService.SendRequestAsync<UpdateBlogDto, string>(HttpMethod.Put, "Blogs", updateBlogDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"Blogs/{id}", default);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SetBlogDisplayStatus(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Get, $"Blogs/SetBlogDisplayStatus/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}
