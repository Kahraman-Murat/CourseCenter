using CourseCenter.WebUI.DTOs.BannerDtos;
using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(CreateUserDto createUserDto)
        {
            var validator = new CreateUserValidator();
            var result = await validator.ValidateAsync(createUserDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(createUserDto);
            }

            var response = await _client.PostAsJsonAsync("users/Create", createUserDto);
            return RedirectToAction("Index","Home");
        }
    }
}
