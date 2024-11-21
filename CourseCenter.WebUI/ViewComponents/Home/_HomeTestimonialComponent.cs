using CourseCenter.WebUI.DTOs.TestimonialDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.ViewComponents.Home
{
    public class _HomeTestimonialComponent(IHttpClientService _httpClientService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultTestimonialDto>>(HttpMethod.Get, "testimonials", default));
    }
}
