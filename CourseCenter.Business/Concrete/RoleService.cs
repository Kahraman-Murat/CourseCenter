using CourseCenter.Business.Abstract;
using CourseCenter.Business.Helpers;
using AutoMapper;
using CourseCenter.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseCenter.DTO.DTOs.RoleDtos;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CourseCenter.Business.Concrete
{
    public class RoleService(RoleManager<AppRole> _roleManager, IMapper _mapper, RolesScannerInAssemblyService _rolesScannerInAssemblyService) : IRoleService
    {
        public async Task<List<ResultRoleDto>> GetAllRoles()
        {
            List<AppRole> roles = await _roleManager.Roles.ToListAsync();
            var mappedRoles = _mapper.Map<List<ResultRoleDto>>(roles);

            return mappedRoles;
        }

        public async Task<ResultRoleDto> GetRoleById(int id)
        {
            AppRole? role = await _roleManager.FindByIdAsync(id.ToString());
            var mappedRole = _mapper.Map<ResultRoleDto>(role);

            return mappedRole;
        }

        public async Task<(bool Success, string[] Messages)> CreateRole(CreateRoleDto createRoleDto)
        {
            if (await _roleManager.RoleExistsAsync(createRoleDto.Name))
                return (false, new[] { "Rol zaten mevcut." });

            var role = _mapper.Map<AppRole>(createRoleDto);
            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
                return (true, new[] { "Rol olusturuldu." });

            return (false, result.Errors.Select(e => e.Description).ToArray());
        }

        public async Task<bool> UpdateRole(UpdateRoleDto updateRoleDto)
        { 
            AppRole? role = await _roleManager.FindByIdAsync(updateRoleDto.Id.ToString());
            if (role == null)
                return false;

            role.Name = updateRoleDto.Name;
            IdentityResult result = await _roleManager.UpdateAsync(role);

            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(int id)
        {
            AppRole? role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return false;

            IdentityResult result = await _roleManager.DeleteAsync(role);

            return result.Succeeded;
        }

        public List<string> GetDefinedRolesInAssembly(Assembly assembly)
        {
            List<string> definedRoles = _rolesScannerInAssemblyService.GetAllDefinedRoles(assembly);           

            return definedRoles;
        }

    }
}
