using CourseCenter.WebUI.DTOs.AuthDtos;
using CourseCenter.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;


namespace CourseCenter.WebUI.Controllers
{
    public class AuthController(IHttpClientService _httpClientService, IRefreshTokenService _refreshTokenService) : Controller
    {
        [HttpGet]
        public IActionResult Login(string? returnUrl = null) => View();

        [HttpPost]
        public async Task<IActionResult> Login(RequestLoginDto requestLoginDto, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ResponseLoginDto tokenResponse = await _httpClientService.SendRequestAsync<RequestLoginDto, ResponseLoginDto>(HttpMethod.Post, "auths/login", requestLoginDto);

                if (tokenResponse != null)
                {                    
                    _refreshTokenService.RemoveTokensCookies();
                    _refreshTokenService.SaveTokensCookies(tokenResponse.AccessToken, tokenResponse.RefreshToken);
                                        
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Gecersiz login denemesi.");
            }
            
            return View(requestLoginDto);
        }
    }
}
