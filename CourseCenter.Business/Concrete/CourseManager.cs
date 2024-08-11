using CourseCenter.Business.Abstract;
using CourseCenter.DataAccess.Abstract;
using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Course> TGetCoursesWithCategories()
        {
            return _courseRepository.GetCoursesWithCategories();
        }

        public void TSetCourseDisplayStatus(int id)
        {
            _courseRepository.SetCourseDisplayStatus(id);
        }
    }
}
