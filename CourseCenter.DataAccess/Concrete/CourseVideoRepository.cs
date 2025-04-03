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
    public class CourseVideoRepository : GenericRepository<CourseVideo>, ICourseVideoRepository
    {
        public CourseVideoRepository(CourseCenterContext context) : base(context)
        {
        }

        public List<CourseVideo> GetVideosWithCourseByCourseId(int id = 0)
        {
            var query = _context.CourseVideos.AsQueryable();
            if (id > 0)
                query = query.Where(b => b.CourseId == id);

            return query.Include(x => x.Course).ToList();
        }
    }
}
