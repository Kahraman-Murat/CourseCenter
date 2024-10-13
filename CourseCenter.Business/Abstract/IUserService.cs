using CourseCenter.DTO.DTOs.UserDtos;
using Microsoft.AspNetCore.Identity;
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

    }
}
