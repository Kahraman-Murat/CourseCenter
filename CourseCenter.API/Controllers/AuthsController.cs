using Azure;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.AuthDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController(IAuthService _authService) : ControllerBase
    {

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(RequestLoginDto requestLoginDto)
        {

            ResponseLoginDto response = await _authService.LoginAsync(requestLoginDto);

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Logout()
        {

            bool status = await _authService.LogoutAsync();

            if (status)
                return StatusCode(StatusCodes.Status200OK, "Cikis islemi başarılı.");

            return StatusCode(StatusCodes.Status400BadRequest, "Cikis isleminde Hata olustu");
        }
    }
}
