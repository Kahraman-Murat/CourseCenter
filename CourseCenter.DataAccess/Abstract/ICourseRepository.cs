using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DataAccess.Abstract
{
    public interface ICourseRepository : IRepository<Course>
    {
        List<Course> GetCoursesWithCategoryUndTeacher();

        void SetCourseDisplayStatus(int id);
    }
}
