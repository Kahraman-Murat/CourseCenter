using CourseCenter.WebUI.DTOs.TeacherSocialMediaDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseCenter.WebUI.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    public class TeacherSocialMediaController(IHttpClientService _httpClientService) : Controller
    {
        public async Task<IActionResult> Index() => View(await _httpClientService.SendRequestAsync<string, List<ResultTeacherSocialMediaDto>>(HttpMethod.Get, $"teacherSocialMedias/GetByTeacherId", default));

        [HttpGet]
        public async Task<IActionResult> Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateTeacherSocialMediaDto createTeacherSocialMediaDto)
        {
            createTeacherSocialMediaDto.TeacherId = 0; //Set in Backend 

            var validator = new CreateTeacherSocialMediaValidator();
            var result = await validator.ValidateAsync(createTeacherSocialMediaDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(createTeacherSocialMediaDto);
            }

            await _httpClientService.SendRequestAsync<CreateTeacherSocialMediaDto, string>(HttpMethod.Post, "TeacherSocialMedias", createTeacherSocialMediaDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id) => View(await _httpClientService.SendRequestAsync<string, UpdateTeacherSocialMediaDto>(HttpMethod.Get, $"TeacherSocialMedias/{id}", default));

        [HttpPost]
        public async Task<IActionResult> Update(UpdateTeacherSocialMediaDto updateTeacherSocialMediaDto)
        {
            var validator = new UpdateTeacherSocialMediaValidator();
            var result = await validator.ValidateAsync(updateTeacherSocialMediaDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(updateTeacherSocialMediaDto);
            }

            await _httpClientService.SendRequestAsync<UpdateTeacherSocialMediaDto, string>(HttpMethod.Put, "TeacherSocialMedias", updateTeacherSocialMediaDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"TeacherSocialMedias/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}
