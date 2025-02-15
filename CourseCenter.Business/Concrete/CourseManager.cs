using CourseCenter.Business.Abstract;
using CourseCenter.DataAccess.Abstract;
using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Concrete
{
    public class CourseManager : GenericManager<Course>, ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseManager(IRepository<Course> _repository, ICourseRepository courseRepository) : base(_repository)
        {
            _courseRepository = courseRepository;
        }

        public List<Course> TGetCoursesWithCategoryUndTeacher()
        {
            return _courseRepository.GetCoursesWithCategoryUndTeacher();
        }

        public List<Course> TGetCoursesWithCategoryUndTeacher(Expression<Func<Course, bool>> filter)
        {
            return _courseRepository.GetCoursesWithCategoryUndTeacher(filter);
        }

        public void TSetCourseDisplayStatus(int id)
        {
            _courseRepository.SetCourseDisplayStatus(id);
        }
    }
}
