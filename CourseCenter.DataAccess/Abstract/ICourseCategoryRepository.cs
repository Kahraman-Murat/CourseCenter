using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DataAccess.Abstract
{
    public interface ICourseCategoryRepository : IRepository<CourseCategory>
    {
        List<CourseCategory> GetCategoriesWithCoursesUndTeacherById(int id);
        void SetCourseCategoryDisplayStatus(int id);
    }
}
