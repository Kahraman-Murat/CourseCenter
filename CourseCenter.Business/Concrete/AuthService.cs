using AutoMapper;
using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.UserDtos;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseCenter.Business.Concrete
{
    public class AuthService(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<AppRole> _roleManager, IMapper _mapper) : IAuthService
    {
        
        public async Task<(bool Success, string[] Errors)> LoginAsync(UserLoginDto userLoginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user == null)
                return (false, new[] { "Kullanıcı adı veya şifre hatalı." });

            var result = await _signInManager.PasswordSignInAsync(user.UserName, userLoginDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                //Token geri döndürülecek
                return (true, null);
            }

            return (false, new[] { "Kullanıcı adı veya şifre hatalı." });
        }

        public async Task<bool> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return true;
        }        

    }
}
