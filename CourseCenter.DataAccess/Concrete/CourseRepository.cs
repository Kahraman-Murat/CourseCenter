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
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(CourseCenterContext _context) : base(_context)
        {
        }

        public List<Course> GetCoursesWithCategoryUndTeacher()
        {
            return _context.Courses.Include(x => x.CourseCategory).Include(t => t.Teacher).ToList();
        }

        public void SetCourseDisplayStatus(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                course.IsShown = !course.IsShown;
            }
            _context.SaveChanges();
        }
    }
}
