using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.AuthDtos;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseCenter.Business.Concrete
{
    public class AuthService(UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager, IMapper _mapper, ITokenService _tokenService, IConfiguration _configuration) : IAuthService
    {

        public async Task<ResponseLoginDto> LoginAsync(RequestLoginDto userLoginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user == null)
                throw new Exception("Kullanıcı adı veya şifre hatalı.");

            bool checkPassword = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);
            if (!checkPassword)
                throw new Exception("Kullanıcı adı veya şifre hatalı.");

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            JwtSecurityToken token = await _tokenService.CreateAccessToken(user, userRoles);
            string refreshToken = _tokenService.CreateRefreshToken();

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(user);
            await _userManager.UpdateSecurityStampAsync(user);

            string _token = new JwtSecurityTokenHandler().WriteToken(token);

            await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", _token);

            return new()
            {
                AccessToken = _token,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };
        }

        public async Task<ResponseTokenDto> RefreshTokenAsync(RequestTokenDto requestTokenDto)
        {
            ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(requestTokenDto.AccessToken);

            string? email = principal.FindFirstValue(ClaimTypes.Email);

            AppUser? user =  await _userManager.FindByEmailAsync(email);
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            if (user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new Exception("Oturum süresi sona ermistir. Lütfen tekrar giris yapin.");

            JwtSecurityToken newAccessToken = await _tokenService.CreateAccessToken(user, userRoles);
            string newRefreshToken = _tokenService.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);


            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken,
            };
        }

        public async Task<bool> RevokeAsync(RequestRevokeDto requestRevokeDto)
        {
            AppUser? user = await _userManager.FindByEmailAsync(requestRevokeDto.Email);
            if (user is null)
                throw new Exception("Kullanici sistemde mevcut degil !");

            user.RefreshToken = null;
            IdentityResult result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task RevokeAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();


            foreach (AppUser user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }
            
        }

    }
}
