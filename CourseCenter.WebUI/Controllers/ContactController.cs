using CourseCenter.WebUI.DTOs.ContactDtos;
using CourseCenter.WebUI.DTOs.MessageDtos;
using CourseCenter.WebUI.DTOs.SubscriberDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Controllers
{
    public class ContactController(IHttpClientService _httpClientService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var map = await _httpClientService.SendRequestAsync<string, List<ResultContactDto>>(HttpMethod.Get, "Contacts", default);
            ViewBag.Map = map.Select(x => x.MapUrl).FirstOrDefault();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] CreateMessageDto createMessageDto)
        {
            createMessageDto.EmailDate = DateTime.Now;

            var validator = new CreateMessageValidator();
            var result = await validator.ValidateAsync(createMessageDto);
            if (!result.IsValid)
            {
                var errors = "";
                ModelState.Clear();
                foreach (var x in result.Errors)
                    errors += x.ErrorMessage + " <br/> ";

                return Ok(new ResponseMessageDto { Success = false, Message = errors });
            }

            ResponseMessageDto response = await _httpClientService.SendRequestAsync<CreateMessageDto, ResponseMessageDto>(HttpMethod.Post, "Messages", createMessageDto);

            return Ok(response);
        }
    }
}
