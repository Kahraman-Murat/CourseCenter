using CourseCenter.API.Validators;
using CourseCenter.Business.Abstract;
using CourseCenter.Business.Concrete;
using CourseCenter.DTO.DTOs.UserDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService _userService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id); ;

            return Ok(user);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateUserDto createUserDto)
        {
            // FluentValidation burada devreye giriyor,
            // ModelState kontrolüne gerek kalmıyor
            var (success, errors) = await _userService.CreateAsync(createUserDto);

            if (success)
                return StatusCode(StatusCodes.Status201Created, "Kayıt başarılı.");

            return BadRequest(new { Errors = errors });
        }

        [HttpGet("GetRolesForUser/{id}")]
        public async Task<IActionResult> GetRolesForUser(int id)
        {
            //var assembly = Assembly.GetExecutingAssembly();
            //var result = await _userService.GetRolesForUserAsync(id, assembly);
            var result = await _userService.GetUserRolesAsync(id);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [HttpPost("AssignRoles")]
        public async Task<IActionResult> AssignRoles(AssignUserRolesDto assignUserRolesDto)
        {
            var result = await _userService.AssignRolesToUserAsync(assignUserRolesDto);
            if (result.Any())
                return Ok();

            return BadRequest();
        }

        [HttpGet("GetUserCountInRoles/{roleName}")]
        public async Task<IActionResult> GetUserCountInRoles(string roleName)
        {
            var userCountInRole = await _userService.GetUserCountInRolesAsync(roleName);

            return Ok(userCountInRole);
        }

        [HttpGet("GetUsersInRole/{roleName}")]
        public async Task<IActionResult> GetUsersInRole(string roleName)
        {
            var usersInRole = await _userService.GetUsersInRoleAsync(roleName);

            return Ok(usersInRole);
        }

    }
}
