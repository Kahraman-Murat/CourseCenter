using CourseCenter.WebUI.DTOs.RoleDtos;
using CourseCenter.WebUI.Helpers;
using CourseCenter.WebUI.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseCenter.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class RoleController(IHttpClientService _httpClientService) : Controller
    {
        [HttpGet]
        private async Task<List<string>> GetDefinedRolesAsync() =>
            await _httpClientService.SendRequestAsync<string, List<string>>(HttpMethod.Get, "Roles/GetDefinedRolesInAssembly", default);
        
        [HttpGet]
        public async Task<IActionResult> Index() =>
            View(await _httpClientService.SendRequestAsync<string, List<ResultRoleDto>>(HttpMethod.Get, "Roles", default));

        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            var roles = await _httpClientService.SendRequestAsync<string, List<ResultRoleDto>>(HttpMethod.Get, "Roles", default);
            var systemDefinedRoles = await GetDefinedRolesAsync();
            var filteredRoles = systemDefinedRoles.Where(roleName => !roles.Any(r => r.Name == roleName)).ToList();
            if (filteredRoles.Any())
                ViewBag.actived = "-- Seçiniz --";
            else
                ViewBag.actived = "Tüm Roller Zaten Etkinleştirildi.";
            ViewBag.definedRoles = new SelectList(filteredRoles);

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
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                var roles = await _httpClientService.SendRequestAsync<string, List<ResultRoleDto>>(HttpMethod.Get, "Roles", default);
                var systemDefinedRoles = await GetDefinedRolesAsync();
                var filteredRoles = systemDefinedRoles.Where(roleName => !roles.Any(r => r.Name == roleName)).ToList();
                ViewBag.definedRoles = new SelectList(filteredRoles);

                return View(createRoleDto);
            }

            await _httpClientService.SendRequestAsync<CreateRoleDto, string>(HttpMethod.Post, "Roles", createRoleDto);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(int id) =>
            View(await _httpClientService.SendRequestAsync<string, UpdateRoleDto>(HttpMethod.Get, $"Roles/{id}", default));

        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var validator = new UpdateRoleValidator();
            var result = await validator.ValidateAsync(updateRoleDto);
            if (!result.IsValid)
            {
                ModelState.Clear();
                foreach (var x in result.Errors)
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);

                return View(updateRoleDto);
            }

            await _httpClientService.SendRequestAsync<UpdateRoleDto, string>(HttpMethod.Put, "Roles", updateRoleDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            await _httpClientService.SendRequestAsync<string, string>(HttpMethod.Delete, $"Roles/{id}", default);

            return RedirectToAction(nameof(Index));
        }

    }
}