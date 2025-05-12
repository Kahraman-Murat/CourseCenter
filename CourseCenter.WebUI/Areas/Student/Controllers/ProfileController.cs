using CourseCenter.WebUI.DTOs.MessageDtos;
using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseCenter.WebUI.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ProfileController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ResultUserWithRolesDto datas =
                await _httpClientService.SendRequestAsync<string, ResultUserWithRolesDto>(HttpMethod.Get, "Users/userProfile", default);
            return View(datas);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int id)
        {

            UpdateUserDto data = await _httpClientService.SendRequestAsync<string, UpdateUserDto>(HttpMethod.Get, $"Users/{id}", default);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateUserDto updateUserDto)
        {
            var validator = new UpdateUserValidator();
            var result = await validator.ValidateAsync(updateUserDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(updateUserDto);
            }

            ResponseMessageDto resultRequest = await _httpClientService.SendRequestAsync<UpdateUserDto, ResponseMessageDto>(HttpMethod.Put, "Users", updateUserDto);

            if (resultRequest != null)
                return RedirectToAction(nameof(Index));

            ViewBag.Errors = "HATA - Bu Email adresi kullanılamaz !";
            return View(updateUserDto);
        }
    }
}
