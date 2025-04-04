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
    public class CourseVideoManager : GenericManager<CourseVideo>, ICourseVideoService
    {
        ICourseVideoRepository _courseVideoRepository;
        public CourseVideoManager(IRepository<CourseVideo> _repository, ICourseVideoRepository courseVideoRepository) : base(_repository)
        {
            _courseVideoRepository = courseVideoRepository;
        }

        public List<CourseVideo> TGetVideosWithCourseByCourseId(int id = 0)
        {
            return _courseVideoRepository.GetVideosWithCourseByCourseId(id);
        }
    }
}
