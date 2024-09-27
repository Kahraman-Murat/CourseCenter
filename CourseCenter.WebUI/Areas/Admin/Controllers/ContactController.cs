using CourseCenter.WebUI.DTOs.ContactDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class ContactController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index() //=>
            //View(await _httpClientService.SendRequestAsync<string, List<ResultContactDto>>(HttpMethod.Get, "Contacts", default));
        {
            var xxxxx = await _httpClientService.SendRequestAsync<string, List<ResultContactDto>>(HttpMethod.Get, "Contacts", default);
            return View(xxxxx);
        }

        [HttpGet]
        public async Task<IActionResult> CreateContact() => View();

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            var validator = new CreateContactValidator();
            var result = await validator.ValidateAsync(createContactDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(createContactDto);
            }

            await _httpClientService.SendRequestAsync<CreateContactDto, string>(HttpMethod.Post, "Contacts", createContactDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id) =>
            View(await _httpClientService.SendRequestAsync<string, UpdateContactDto>(HttpMethod.Get, $"Contacts/{id}", default));

        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            var validator = new UpdateContactValidator();
            var result = await validator.ValidateAsync(updateContactDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(updateContactDto);
            }

            await _httpClientService.SendRequestAsync<UpdateContactDto, string>(HttpMethod.Put, "Contacts", updateContactDto);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> DeleteContact(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"Contacts/{id}", default);

            return RedirectToAction(nameof(Index));
        }
    }
}
