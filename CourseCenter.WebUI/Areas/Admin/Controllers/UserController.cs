using CourseCenter.WebUI.DTOs.UserDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class UserController : Controller
    {

        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultUserDto>>("Users");
            return View(datas);
        }

        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {            
            var datas = await _client.GetFromJsonAsync<ResultRolesForUserDto>($"Users/GetRolesForUser/{id}");
            if (!datas.UserExists)
                throw new Exception("Hata olustu");

            TempData["UserId"] = id;
            return View(datas.RolesForUserDtos);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RolesForUserDto> rolesForUserDto)
        {
            AssignRolesToUserDto newRolesForUser = new();
            newRolesForUser.UserId = (int)TempData["UserId"];
            newRolesForUser.RolesForUserDtos = rolesForUserDto;

            var datas = await _client.PostAsJsonAsync<AssignRolesToUserDto>("Users/AssignRoles", newRolesForUser); 
            //if(datas.StatusCode == HttpStatusCode.BadRequest)
            //    return RedirectToAction(nameof(Error));

            return RedirectToAction(nameof(Index));
        }
                
    }
}