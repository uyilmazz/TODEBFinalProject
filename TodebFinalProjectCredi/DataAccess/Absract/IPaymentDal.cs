using Core.DataAccess.MongoDb;
using DTO.Concrete;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Absract
{
    public interface IPaymentDal : IDocumentRepository<Payment>
    {
        List<PaymentDetailDto> GetAllPaymentDetail(Expression<Func<Payment, bool>> expression = null);
    }
}
