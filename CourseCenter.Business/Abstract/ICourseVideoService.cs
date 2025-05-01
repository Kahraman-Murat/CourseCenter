using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Abstract
{
    public interface ICourseVideoService : IGenericService<CourseVideo>
    {
        List<CourseVideo> TGetVideosWithCourseByCourseId(int id = 0);
        List<CourseVideo> TGetVideosWithCourseByTeacherId(int id = 0);
    }
}
