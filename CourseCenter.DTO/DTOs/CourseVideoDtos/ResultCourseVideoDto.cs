using CourseCenter.DTO.DTOs.CourseDtos;

namespace CourseCenter.DTO.DTOs.CourseVideoDtos
{
    public class ResultCourseVideoDto
    {
        public int Id { get; set; }
        public int VideoNumber { get; set; }
        public string VideoTitle { get; set; }
        public string VideoContent { get; set; }
        public string VideoUrl { get; set; }

        public int CourseId { get; set; }
        public ResultCourseDto Course { get; set; }
    }
}
