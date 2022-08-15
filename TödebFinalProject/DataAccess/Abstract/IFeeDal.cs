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
    public interface IFeeDal : IEntityRepository<Fee>
    {
        List<FeeDetailDto> GetAllFeeDetailDto(Expression<Func<Fee, bool>> filter = null);
        FeeDetailDto GetFeeDetailDto(Expression<Func<Fee, bool>> filter);
    }
}
