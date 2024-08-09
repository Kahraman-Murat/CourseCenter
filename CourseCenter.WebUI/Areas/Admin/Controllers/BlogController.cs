using CourseCenter.WebUI.DTOs.BlogCategoryDtos;
using CourseCenter.WebUI.DTOs.BlogDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class BlogController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();

        private async Task<List<ResultBlogCategoryDto>> GetBlogCategoriesAsync()
        {
            return await _client.GetFromJsonAsync<List<ResultBlogCategoryDto>>("BlogCategories");
        }

        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultBlogDto>>("Blogs");
            return View(datas);
        }

        public async Task<IActionResult> DeleteBlog(int id)
        {
            await _client.DeleteAsync($"Blogs/{id}");
            return RedirectToAction(nameof(Index));
        }

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
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
                                
                var categories = await GetBlogCategoriesAsync();
                ViewBag.blogCategories = new SelectList(categories, "Id", "Name");
                                
                return View(createBlogDto);
            }

            await _client.PostAsJsonAsync("Blogs", createBlogDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBlog(int id)
        {
            var categories = await GetBlogCategoriesAsync();
            ViewBag.blogCategories = new SelectList(categories, "Id", "Name");

            var data = await _client.GetFromJsonAsync<UpdateBlogDto>($"Blogs/{id}");
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
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                var categories = await GetBlogCategoriesAsync();
                ViewBag.blogCategories = new SelectList(categories, "Id", "Name");

                return View(updateBlogDto);
            }

            await _client.PutAsJsonAsync("Blogs", updateBlogDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> SetBlogDisplayStatus(int id)
        {
            await _client.GetAsync($"Blogs/SetBlogDisplayStatus/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
