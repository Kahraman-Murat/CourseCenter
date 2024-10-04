using CourseCenter.WebUI.DTOs.AboutDtos;
using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class UserController(IHttpClientService _httpClientService) : Controller
    {


        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultUserDto>>(HttpMethod.Get, "Users", default));

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            ResultUserRolesDto datas = await _httpClientService.SendRequestAsync<string, ResultUserRolesDto>(HttpMethod.Get, $"Users/GetRolesForUser/{id}", default);
            if (datas is null)
                throw new Exception("Hata olustu");

            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(ResultUserRolesDto resultUserRolesDto)
        {
            AssignUserRolesDto newUserRoles = new();
            newUserRoles.UserId = resultUserRolesDto.UserId;
            newUserRoles.RoleStateDtos = resultUserRolesDto.RoleStateDtos;
                        
            var datas = await _httpClientService.SendRequestAsync<AssignUserRolesDto, string>(HttpMethod.Post, "Users/AssignRoles", newUserRoles);            

            return RedirectToAction(nameof(Index));
        }
                
    }
}