using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Abstract
{
    public interface ICourseService : IGenericService<Course>
    {
        List<Course> TGetCoursesWithCategoryUndTeacher();

        void TSetCourseDisplayStatus(int id);
    }
}
