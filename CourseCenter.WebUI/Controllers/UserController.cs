//using AutoMapper;
using CourseCenter.WebUI.DTOs.AuthDtos;
using CourseCenter.WebUI.DTOs.BannerDtos;
using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CourseCenter.WebUI.Controllers
{
    public class UserController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public IActionResult Signup() => View();

        [HttpPost]
        public async Task<IActionResult> Signup(CreateUserDto createUserDto)
        {
            var validator = new CreateUserValidator();
            var result = await validator.ValidateAsync(createUserDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(createUserDto);
            }

            await _httpClientService.SendRequestAsync<CreateUserDto, object>(HttpMethod.Post, "users/Create", createUserDto);

            return RedirectToAction("Index", "Home");
        }
    }
}
