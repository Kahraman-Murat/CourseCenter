using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Abstract
{
    public interface ICourseRegisterService : IGenericService<CourseRegister>
    {
        List<CourseRegister> TGetRegistersWithCourseUndCategory(Expression<Func<CourseRegister,bool>> filter);

    }
}
