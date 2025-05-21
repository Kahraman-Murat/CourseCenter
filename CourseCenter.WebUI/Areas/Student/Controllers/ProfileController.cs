using CourseCenter.WebUI.DTOs.MessageDtos;
using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ProfileController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ResultUserWithRolesDto data = await _httpClientService.SendRequestAsync<string, ResultUserWithRolesDto>(HttpMethod.Get, "Users/userProfile", default);

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int id)
        {
            UpdateUserDto data = await _httpClientService.SendRequestAsync<string, UpdateUserDto>(HttpMethod.Get, "Users/userProfile", default);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateUserDto updateUserDto, IFormFile imageFile)
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

            // Resim base64 formatına çevrilir
            if (imageFile != null && imageFile.Length > 0)
            {
                using var ms = new MemoryStream();
                await imageFile.CopyToAsync(ms);
                var bytes = ms.ToArray();
                updateUserDto.Base64Image = Convert.ToBase64String(bytes);
                updateUserDto.ImageFileName = imageFile.FileName;
            }

            // JSON olarak gönderilir (multipart yok)
            var resultRequest = await _httpClientService
                .SendRequestAsync<UpdateUserDto, ResponseMessageDto>(HttpMethod.Put, "Users", updateUserDto);

            if (resultRequest != null)
                return RedirectToAction(nameof(Index));

            ViewBag.Errors = "HATA - Bu Email adresi kullanılamaz !";
            return View(updateUserDto);
        }
    }
}
