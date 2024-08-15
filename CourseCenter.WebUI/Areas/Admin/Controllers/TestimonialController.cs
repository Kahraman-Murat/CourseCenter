using CourseCenter.WebUI.DTOs.TestimonialDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class TestimonialController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();

        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultTestimonialDto>>("Testimonials");
            return View(datas);
        }

        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            await _client.DeleteAsync($"Testimonials/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateTestimonial()
        {            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var validator = new CreateTestimonialValidator();
            var result = await validator.ValidateAsync(createTestimonialDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(createTestimonialDto);
            }

            await _client.PostAsJsonAsync("Testimonials", createTestimonialDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var data = await _client.GetFromJsonAsync<UpdateTestimonialDto>($"Testimonials/{id}");
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            var validator = new UpdateTestimonialValidator();
            var result = await validator.ValidateAsync(updateTestimonialDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(updateTestimonialDto);
            }

            await _client.PutAsJsonAsync("Testimonials", updateTestimonialDto);
            return RedirectToAction(nameof(Index));
        }
    }
}

