using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.UserDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IUserService _userService) : ControllerBase
    {

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {

            var (success, errors) = await _userService.LoginAsync(userLoginDto);

            if (success)
                return Ok("Giriş başarılı.");

            return Unauthorized(new { Errors = errors });

        }
    }
}
