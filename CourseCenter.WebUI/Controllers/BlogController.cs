using CourseCenter.WebUI.DTOs.BlogDtos;
using CourseCenter.WebUI.DTOs.SubscriberDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using System;

namespace CourseCenter.WebUI.Controllers
{
    public class BlogController(IHttpClientService _httpClientService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe([FromBody] CreateSubscriberDto createSubscriberDto)
        {
            createSubscriberDto.IsActive = false;

            var validator = new CreateSubscriberValidator();
            var result = await validator.ValidateAsync(createSubscriberDto);
            if (!result.IsValid)
            {
                var errors = "";
                ModelState.Clear();
                foreach (var x in result.Errors)
                    errors += x.ErrorMessage + " <br/> ";

                return Ok(new ResponseSubscribeDto { Success = false, Message = errors });
            }

            ResponseSubscribeDto response = await _httpClientService.SendRequestAsync<CreateSubscriberDto, ResponseSubscribeDto>(HttpMethod.Post, "Subscribers", createSubscriberDto);

            return Ok(response);
        }

        [Route("Blog/Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            ResultBlogDto resultBlogDto = await _httpClientService.SendRequestAsync<string, ResultBlogDto>(HttpMethod.Get, $"blogs/{id}", default);

            return View(resultBlogDto);
        }

        [Route("Blog/BlogsByCategoryId/{id}")]
        public async Task<IActionResult> BlogsByCategoryId(int id)
        {
            List<ResultBlogDto> resultBlogDto = await _httpClientService.SendRequestAsync<string, List<ResultBlogDto>>(HttpMethod.Get, $"blogs/GetByCategoryId/{id}", default);

            ViewBag.SelectedCategory = resultBlogDto.Select(x => x.BlogCategory.Name).FirstOrDefault();

            return View(resultBlogDto);
        }
    }
}
