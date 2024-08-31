using Microsoft.AspNetCore.Identity;
using CourseCenter.DTO.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseCenter.Business.Abstract
{
    public interface IUserService
    {
        Task<List<ResultUserDto>> GetAllAsync();
        Task<ResultUserDto> GetByIdAsync(int id);
        Task<(bool Success, string[] Errors)> CreateAsync(CreateUserDto createUserDto);
        Task<ResultRolesForUserDto> GetRolesForUserAsync(int id);
        Task<List<IdentityResult>> AssignRolesToUserAsync(AssignRolesToUserDto assignRolesToUserDto);

    }
}
