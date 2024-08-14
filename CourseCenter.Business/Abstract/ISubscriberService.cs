using CourseCenter.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCenter.Business.Abstract
{
    public interface ISubscriberService : IGenericService<Subscriber>
    {
        void TSetSubscriberStatus(int id);
    }
}
