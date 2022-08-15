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
    public interface IBillDal : IEntityRepository<Bill>
    {
        List<BillDetailDto> GetAllBillDetailDto(Expression<Func<Bill, bool>> filter = null);
        BillDetailDto GetBillDetailDto(Expression<Func<Bill, bool>> filter);
    }
}
