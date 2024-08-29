using CourseCenter.API.Validators;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.UserDtos;
using CourseCenter.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                return Ok("Kayıt başarılı.");

            return BadRequest(new { Errors = errors });
        }

        [HttpGet("GetRolesForUser/{id}")]
        public async Task<IActionResult> GetRolesForUser(int id)
        {
            var result = await _userService.GetRolesForUserAsync(id);
            if (result.UserExists)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("AssignRoles")]
        public async Task<IActionResult> AssignRoles(AssignRolesToUserDto assignRolesToUserDto)
        {
            var result = await _userService.AssignRolesToUserAsync(assignRolesToUserDto);
            if (result.Any())
                return Ok(result);

            return BadRequest(result);
        }
    }
}
