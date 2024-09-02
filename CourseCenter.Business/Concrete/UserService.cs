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
    public class UserService(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager, RoleManager<AppRole> _roleManager, IMapper _mapper) : IUserService
    {
        public async Task<List<ResultUserDto>> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            var mappedUsers = _mapper.Map<List<ResultUserDto>>(users);

            return mappedUsers;
        }

        public async Task<ResultUserDto> GetByIdAsync(int id)
        {
            AppUser? user = await _userManager.FindByIdAsync(id.ToString());
            var mappedUser = _mapper.Map<ResultUserDto>(user);

            return mappedUser;
        }

        public async Task<(bool Success, string[] Errors)> CreateAsync(CreateUserDto createUserDto)
        {
            var userExist = await _userManager.FindByEmailAsync(createUserDto.Email);
            if (userExist is not null)
                return (false, new[] { "Böyle bir kullanici zaten var!" });
            
            var user = new AppUser
            {
                FullName = createUserDto.FullName,
                UserName = createUserDto.UserName,
                Email = createUserDto.Email
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (result.Succeeded)
                return (true, null);

            return (false, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<ResultRolesForUserDto> GetRolesForUserAsync(int id)
        {
            ResultRolesForUserDto result = new();

            AppUser? user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return result;

            result.UserExists = true;

            var userRoles = await _userManager.GetRolesAsync(user);

            List<AppRole> allRoles = await _roleManager.Roles.ToListAsync();
            if (allRoles.Any())
            {
                foreach (var role in allRoles)
                {
                    RolesForUserDto rolesForUser = new();

                    rolesForUser.RoleId = role.Id;
                    rolesForUser.RoleName = role.Name;
                    rolesForUser.RoleExist = userRoles.Contains(role.Name);

                    result.RolesForUserDtos.Add(rolesForUser);
                }
            }

            return result;

        }

        public async Task<List<IdentityResult>> AssignRolesToUserAsync(AssignRolesToUserDto assignRolesToUserDto)
        {
            List<IdentityResult> result = new();

            AppUser? user = await _userManager
                .FindByIdAsync(assignRolesToUserDto.UserId.ToString());
            if (user == null)
                return result;

            foreach (var item in assignRolesToUserDto.RolesForUserDtos)
            {
                if (item.RoleExist)
                    result.Add(await _userManager
                        .AddToRoleAsync(user, item.RoleName));
                else
                    result.Add(await _userManager
                        .RemoveFromRoleAsync(user, item.RoleName));
            }

            return result;
        }

    }
}
