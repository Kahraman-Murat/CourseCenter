using CourseCenter.WebUI.DTOs.AboutDtos;
using CourseCenter.WebUI.DTOs.TestimonialDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class TestimonialController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultTestimonialDto>>(HttpMethod.Get, "Testimonials", default));

        [HttpGet]
        public async Task<IActionResult> CreateTestimonial() => View();

        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var validator = new CreateTestimonialValidator();
            var result = await validator.ValidateAsync(createTestimonialDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(createTestimonialDto);
            }

            await _httpClientService.SendRequestAsync<CreateTestimonialDto, string>(HttpMethod.Post, "Testimonials", createTestimonialDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id) =>
            View(await _httpClientService.SendRequestAsync<string, UpdateTestimonialDto>(HttpMethod.Get, $"Testimonials/{id}", default));

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

            await _httpClientService.SendRequestAsync<UpdateTestimonialDto, string>(HttpMethod.Put, "Testimonials", updateTestimonialDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"Testimonials/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}

