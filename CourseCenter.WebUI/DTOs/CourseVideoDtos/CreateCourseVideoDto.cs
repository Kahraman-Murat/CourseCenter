namespace CourseCenter.WebUI.DTOs.CourseVideoDtos
{
    public class CreateCourseVideoDto
    {
        public int VideoNumber { get; set; }
        public string VideoTitle { get; set; }
        public string VideoContent { get; set; }
        public string VideoUrl { get; set; }

        public int CourseId { get; set; }
    }
}
