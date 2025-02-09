using CourseCenter.DataAccess.Abstract;
using CourseCenter.DataAccess.Context;
using CourseCenter.DataAccess.Repositories;
using CourseCenter.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.DataAccess.Concrete
{
    public class CourseRegisterRepository : GenericRepository<CourseRegister>, ICourseRegisterRepository
    {
        public CourseRegisterRepository(CourseCenterContext _context) : base(_context)
        {
        }

        public List<CourseRegister> GetRegistersWithCourseUndCategory(Expression<Func<CourseRegister, bool>> filter)
        {
            return _context.CourseRegisters.Where(filter).Include(x => x.Course).ThenInclude(c => c.CourseCategory).Include(t => t.Course.Teacher).ToList();
        }
    }
}
