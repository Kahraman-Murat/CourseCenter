using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Abstract
{
    public interface ICourseService : IGenericService<Course>
    {
        List<Course> TGetCoursesWithCategoryUndTeacher();
        List<Course> TGetCoursesWithCategoryUndTeacher(Expression<Func<Course,bool>> filter);

        void TSetCourseDisplayStatus(int id);
    }
}
