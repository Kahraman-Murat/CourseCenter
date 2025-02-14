using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DataAccess.Abstract
{
    public interface ICourseRepository : IRepository<Course>
    {
        List<Course> GetCoursesWithCategoryUndTeacher();
        List<Course> GetCoursesWithCategoryUndTeacher(Expression<Func<Course,bool>> filter = null);

        void SetCourseDisplayStatus(int id);
    }
}
