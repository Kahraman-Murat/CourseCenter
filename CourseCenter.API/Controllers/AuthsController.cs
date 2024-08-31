using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IAuthService _userService) : ControllerBase
    {

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {

            var (success, errors) = await _userService.LoginAsync(userLoginDto);

            if (success)
                return Ok("Giriş başarılı.");

            return Unauthorized(new { Errors = errors });

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {

            bool status = await _userService.LogoutAsync();

            if (status)
                return Ok("Cikis islemi başarılı.");

            return BadRequest("Cikis isleminde Hata olustu");

        }
    }
}
