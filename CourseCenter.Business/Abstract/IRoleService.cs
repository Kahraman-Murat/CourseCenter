using CourseCenter.DTO.DTOs.RoleDtos;
using CourseCenter.Entity.Entities.Identity;

namespace CourseCenter.Business.Abstract
{
    public interface IRoleService
    {
        Task<List<AppRole>> GetAllRoles();
        Task<(string id, string name)> GetRoleById(int id);
        Task<(bool Success, string[] Messages)> CreateRole(CreateRoleDto createRoleDto);
        Task<bool> UpdateRole(UpdateRoleDto updateRoleDto);
        Task<bool> DeleteRole(int id);
    }
}
