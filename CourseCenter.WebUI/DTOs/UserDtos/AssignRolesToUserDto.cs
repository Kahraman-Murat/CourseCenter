

namespace CourseCenter.WebUI.DTOs.UserDtos
{
    public class AssignRolesToUserDto
    {
        public int UserId { get; set; }
        public List<RolesForUserDto> RolesForUserDtos { get; set; } = new List<RolesForUserDto>();
    }

}
