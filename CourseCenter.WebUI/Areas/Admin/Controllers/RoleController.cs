using CourseCenter.WebUI.DTOs.RoleDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class RoleController : Controller
    {
        private readonly HttpClient _client = HttpClientInstance.CreateClient();
        public async Task<IActionResult> Index()
        {
            var datas = await _client.GetFromJsonAsync<List<ResultRoleDto>>("Roles");
            return View(datas);
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            await _client.DeleteAsync($"Roles/{id}");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleDto createRoleDto)
        {
            var validator = new CreateRoleValidator();
            var result = await validator.ValidateAsync(createRoleDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(createRoleDto);
            }

            await _client.PostAsJsonAsync("Roles", createRoleDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(int id)
        {
            var datas = await _client.GetFromJsonAsync<UpdateRoleDto>($"Roles/{id}");
            return View(datas);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var validator = new UpdateRoleValidator();
            var result = await validator.ValidateAsync(updateRoleDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }

                return View(updateRoleDto);
            }

            await _client.PutAsJsonAsync("Roles", updateRoleDto);
            return RedirectToAction(nameof(Index));
        }



    }
}