using Core.DataAccess;
using Dto.Concrete;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPersonDal : IEntityRepository<Person>
    {
        PersonDetailDto GetPersonDetail(int id);
        List<PersonDetailDto> GetAllPersonDetail();
        Person GetPersonWithPassword(Expression<Func<Person, bool>> filter);
    }
}
