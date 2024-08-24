using Microsoft.AspNetCore.Identity;
using CourseCenter.DTO.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Abstract
{
    public interface IUserService
    {
        Task<(bool Success, string[] Errors)> RegisterAsync(UserRegisterDto userRegisterDto);
        Task<IdentityResult> LoginAsync(UserLoginDto userLoginDto);
        Task<bool> LogoutAsync();
        Task<IdentityResult> CreateRoleAsync(UserRoleDto userRoleDto);
        Task<IdentityResult> AssignRoleAsync(UserRoleDto userRoleDto);

    }
}
