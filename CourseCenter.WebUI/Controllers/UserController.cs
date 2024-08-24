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
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Signup(UserRegisterDto userRegisterDto)
        {
            var validator = new UserRegisterValidator();
            var result = await validator.ValidateAsync(userRegisterDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(userRegisterDto);
            }

            var response = await _client.PostAsJsonAsync("users/Register", userRegisterDto);
            return RedirectToAction("Index","Home");
        }
    }
}
