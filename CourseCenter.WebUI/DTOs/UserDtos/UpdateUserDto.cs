namespace CourseCenter.WebUI.DTOs.UserDtos
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ImageUrl { get; set; }
        public string Base64Image { get; set; }
        public string ImageFileName { get; set; }

    }
}
