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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseCenter.Business.Concrete
{
    public class UserService(UserManager<AppUser> _userManager, RoleManager<AppRole> _roleManager, IRoleService _roleService, IMapper _mapper) : IUserService
    {
        public async Task<List<ResultUserDto>> GetAllAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            List<ResultUserDto> mappedUsers = _mapper.Map<List<ResultUserDto>>(users);

            return mappedUsers;
        }

        public async Task<ResultUserDto> GetByIdAsync(int id)
        {
            AppUser? user = await _userManager.FindByIdAsync(id.ToString());
            ResultUserDto mappedUser = _mapper.Map<ResultUserDto>(user);

            return mappedUser;
        }

        public async Task<List<ResultUserWithRolesDto>> GetUsersWithRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<ResultUserWithRolesDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userList.Add(new ResultUserWithRolesDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return userList;
        }

        public async Task<(bool Success, string[] Errors)> CreateAsync(CreateUserDto createUserDto)
        {
            AppUser? userExist = await _userManager.FindByEmailAsync(createUserDto.Email);
            if (userExist is not null)
                return (false, new[] { "Böyle bir kullanici zaten var!" });

            AppUser user = _mapper.Map<AppUser>(createUserDto);
            user.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                    await _roleManager.CreateAsync(new AppRole
                    {
                        Name = "User",
                        NormalizedName = "USER",
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                    });

                await _userManager.AddToRoleAsync(user, "User");

                return (true, null);
            }                

            return (false, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<(bool Success, string[] Errors)> UpdateAsync(UpdateUserDto updateUserDto)
        {
            AppUser? user = await _userManager.FindByIdAsync(updateUserDto.Id.ToString());

            if (user == null)
                return (false, new[] { "Kullanıcı bulunamadı!" });

            if(user.Email != updateUserDto.Email)
            {
                AppUser? emailExist = await _userManager.FindByEmailAsync(updateUserDto.Email);
                if (emailExist is not null)
                    return (false, new[] { "Başka bir Email adresi kullanmalısınız!" });

                user.Email = updateUserDto.Email;
                user.EmailConfirmed = false;
            }

            user.FullName = updateUserDto.FullName;
            user.UserName = updateUserDto.UserName;

            IdentityResult updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
                return (true, null);

            return (false, updateResult.Errors.Select(e => e.Description).ToArray());
        }
        public async Task<ResultUserRolesDto> GetUserRolesAsync(int id)//, Assembly assembly
        {
            ResultUserRolesDto result = new();

            AppUser? user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return result;

            result.UserId = user.Id;
            result.FullName = user.FullName;

            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            /*List<string>allRoles=_roleService.GetDefinedRolesInAssembly(assembly);*/
            
            List<AppRole> allRoles = await _roleManager.Roles.ToListAsync();
            if (allRoles.Any())
            {
                foreach (AppRole role in allRoles)
                {
                    RoleStateDto roleStateDto = new();

                    roleStateDto.RoleId = role.Id;
                    roleStateDto.RoleName = role.Name;
                    roleStateDto.RoleExist = userRoles.Contains(role.Name);

                    result.RoleStateDtos.Add(roleStateDto);
                }
            }

            return result;

        }

        public async Task<List<IdentityResult>> AssignRolesToUserAsync(AssignUserRolesDto assignUserRolesDto)
        {
            List<IdentityResult> result = new();

            AppUser? user = await _userManager
                .FindByIdAsync(assignUserRolesDto.UserId.ToString());
            if (user == null)
                return result;

            foreach (RoleStateDto item in assignUserRolesDto.RoleStateDtos)
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

        public async Task<int> GetUserCountInRolesAsync(string roleName)
        {            
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            return usersInRole.Count();            
        }

        public async Task<List<ResultUserDto>> GetUsersInRoleAsync(string roleName)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            List<ResultUserDto> users = _mapper.Map<List<ResultUserDto>>(usersInRole);
            return users;
        }

        public async Task<List<ResultUserSocialMediasDto>> GetLast4TeachersAsync()
        {
            var users = await _userManager.Users.Include(x=> x.TeacherSocialMedias).ToListAsync();
            var teachers = users.Where(user => _userManager.IsInRoleAsync(user, "Teacher").Result).OrderByDescending(x=>x.Id).Take(4).ToList();
            return _mapper.Map<List<ResultUserSocialMediasDto>>(teachers);
        }

        public async Task<List<ResultUserSocialMediasDto>> GetTeachersWithSocialMediaAsync(int page)
        {
            //var teachers = _userManager.GetUsersInRoleAsync("Teacher").Result
            //    .AsQueryable().Skip((page-1)*4).Take(6)
            //    .Include(x => x.TeacherSocialMedias)
            //    .OrderByDescending(x => x.Id).ToList();
            //return _mapper.Map<List<ResultUserSocialMediasDto>>(teachers);

            var users = await _userManager.Users.Include(x => x.TeacherSocialMedias).ToListAsync();
            var teachers = users.Where(user => _userManager.IsInRoleAsync(user, "Teacher").Result).OrderByDescending(x => x.Id).Skip((page - 1) * 4).Take(6).ToList();
            return _mapper.Map<List<ResultUserSocialMediasDto>>(teachers);
            
        }
    }
}
