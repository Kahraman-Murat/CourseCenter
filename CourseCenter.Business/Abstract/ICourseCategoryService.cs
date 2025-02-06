using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Abstract
{
    public interface ICourseCategoryService : IGenericService<CourseCategory>
    {
        List<CourseCategory> TGetCategoriesWithCoursesUndTeacherById(int id);
        void TSetCourseCategoryDisplayStatus(int id);
    }
}

