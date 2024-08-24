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
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            // FluentValidation burada devreye giriyor,
            // ModelState kontrolüne gerek kalmıyor
            var (success, errors) = await _userService.RegisterAsync(userRegisterDto);

            if (success)
                return Ok("Kayıt başarılı.");

            return BadRequest(new { Errors = errors });
        }
    }
}
