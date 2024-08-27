using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.RoleDtos;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService _roleService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _roleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _roleService.GetRoleById(id);
            if (result.id == "" && result.name =="" )
                return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDto createRoleDto)
        {
            var result = await _roleService.CreateRole(createRoleDto);
            if (!result.Success)
                return BadRequest(result.Messages);

            return Ok(result.Messages);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleDto updateRoleDto)
        {
            var result = await _roleService.UpdateRole(updateRoleDto);
            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _roleService.DeleteRole(id);
            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}

