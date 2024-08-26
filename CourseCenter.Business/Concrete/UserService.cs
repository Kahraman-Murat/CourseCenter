using CourseCenter.Business.Abstract;
using CourseCenter.DTO.DTOs.UserDtos;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseCenter.Business.Concrete
{
    public class UserService(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<AppRole> _roleManager) : IUserService
    {   

        public async Task<(bool Success, string[] Errors)> RegisterAsync(UserRegisterDto userRegisterDto)
        {
            var user = new AppUser
            {
                FullName = userRegisterDto.FullName,
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email
            };

            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);

            if (result.Succeeded)
                return (true, null);

            return (false, result.Errors.Select(e => e.Description).ToArray());
        }

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

        public Task<IdentityResult> AssignRoleAsync(UserRoleDto userRoleDto)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateRoleAsync(UserRoleDto userRoleDto)
        {
            throw new NotImplementedException();
        }

    }
}
