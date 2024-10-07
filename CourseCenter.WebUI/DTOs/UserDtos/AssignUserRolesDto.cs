namespace CourseCenter.WebUI.DTOs.UserDtos
{
    public class AssignUserRolesDto
    {
        public int UserId { get; set; }
        public List<RoleStateDto> RoleStateDtos { get; set; } = new List<RoleStateDto>();
    }

}
