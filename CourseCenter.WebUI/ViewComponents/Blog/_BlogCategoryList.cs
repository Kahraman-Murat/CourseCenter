using CourseCenter.WebUI.DTOs.BlogCategoryDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Blog
{
    public class _BlogCategoryList(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ResultBlogCategoryDto> categorList = await _httpClientService.SendRequestAsync<string, List<ResultBlogCategoryDto>>(HttpMethod.Get, "blogCategories", default);

            var blogCategories = (from blogCategory in categorList
                                  select new BlogCategoryWithCountViewModel
                                  {
                                      CategoryId = blogCategory.Id,
                                      CategoryName = blogCategory.Name,
                                      BlogCount = blogCategory.Blogs.Count
                                  }).ToList();

            return View(blogCategories);
        }
    }


}