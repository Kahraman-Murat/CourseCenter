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
        public async Task<IActionResult> RefreshTokenCheck(RequestTokenDto requestTokenDto)
        {
            ResponseTokenDto response = await _authService.RefreshTokenAsync(requestTokenDto);

            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Revoke(RequestRevokeDto requestRevokeDto)
        {
            bool status = await _authService.RevokeAsync(requestRevokeDto);
            if (status)
                return StatusCode(StatusCodes.Status200OK, "Cikis islemi başarılı.");

            return StatusCode(StatusCodes.Status400BadRequest, "Cikis isleminde Hata olustu");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RevokeAll()
        {
            await _authService.RevokeAllAsync();

            return StatusCode(StatusCodes.Status200OK, "Cikis islemleri başarılı.");
        }
    }
}
