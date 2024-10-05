using CourseCenter.DTO.DTOs.RoleDtos;
using CourseCenter.Entity.Entities.Identity;
using System.Reflection;

namespace CourseCenter.Business.Abstract
{
    public interface IRoleService
    {
        Task<List<ResultRoleDto>> GetAllRoles();
        Task<ResultRoleDto> GetRoleById(int id);
        List<string> GetDefinedRolesInAssembly(Assembly assembly);
        Task<(bool Success, string[] Messages)> CreateRole(CreateRoleDto createRoleDto);
        Task<bool> UpdateRole(UpdateRoleDto updateRoleDto);
        Task<bool> DeleteRole(int id);
    }
}
