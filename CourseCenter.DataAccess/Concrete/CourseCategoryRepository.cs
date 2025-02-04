using CourseCenter.DataAccess.Abstract;
using CourseCenter.DataAccess.Context;
using CourseCenter.DataAccess.Repositories;
using CourseCenter.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DataAccess.Concrete
{
    public class CourseCategoryRepository : GenericRepository<CourseCategory>, ICourseCategoryRepository
    {
        public CourseCategoryRepository(CourseCenterContext _context) : base(_context)
        {

        }

        public List<CourseCategory> GetCategoriesWithCoursesUndTeacherById(int id)
        {
            var query = _context.CourseCategories;
            if(id> 0)
                query.Where(x=>x.Id == id);

            return query.Include(x => x.Courses).ThenInclude(x=>x.Teacher).ToList();
        }

        public void SetCourseCategoryDisplayStatus(int id)
        {
            var courseCategory = _context.CourseCategories.Find(id);
            if (courseCategory != null)
            {
                courseCategory.IsShown = !courseCategory.IsShown;
            }
            _context.SaveChanges();

        }
    }
}
