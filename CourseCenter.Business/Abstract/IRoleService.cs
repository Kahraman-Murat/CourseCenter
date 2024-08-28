using CourseCenter.DTO.DTOs.RoleDtos;
using CourseCenter.Entity.Entities.Identity;

namespace CourseCenter.Business.Abstract
{
    public interface IRoleService
    {
        Task<List<ResultRoleDto>> GetAllRoles();
        Task<ResultRoleDto> GetRoleById(int id);
        Task<(bool Success, string[] Messages)> CreateRole(CreateRoleDto createRoleDto);
        Task<bool> UpdateRole(UpdateRoleDto updateRoleDto);
        Task<bool> DeleteRole(int id);
    }
}
