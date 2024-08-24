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

namespace CourseCenter.Business.Concrete
{
    public class UserService(UserManager<AppUser> _userManager) : IUserService
    {   //, SignInManager<AppUser> _signInManager, RoleManager<AppRole> _roleManager

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

        public Task<IdentityResult> LoginAsync(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogoutAsync()
        {
            throw new NotImplementedException();
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
