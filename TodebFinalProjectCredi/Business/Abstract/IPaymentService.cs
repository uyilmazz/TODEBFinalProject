using Core.Utilities.Results;
using DTO.Concrete;
using Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IResult Add(PaymentDto paymentDto);
        IResult Update(Payment model);
        IResult Delete(ObjectId id);
        IDataResult<Payment> Get(ObjectId id);
        IDataResult<List<Payment>> GetAll();
    }
}
