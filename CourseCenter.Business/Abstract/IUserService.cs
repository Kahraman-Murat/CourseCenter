using CourseCenter.DTO.DTOs.UserDtos;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CourseCenter.Business.Abstract
{
    public interface IUserService
    {
        Task<List<ResultUserDto>> GetAllAsync();
        Task<ResultUserDto> GetByIdAsync(int id);
        Task<(bool Success, string[] Errors)> CreateAsync(CreateUserDto createUserDto);
        Task<ResultUserRolesDto> GetUserRolesAsync(int id);//, Assembly assembly
        Task<List<IdentityResult>> AssignRolesToUserAsync(AssignUserRolesDto assignUserRolesDto);
        Task<int> GetUserCountInRolesAsync(string roleName);
        Task<List<AppUser>> GetUsersInRoleAsync(string roleName);

    }
}
