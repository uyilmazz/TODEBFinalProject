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
    public interface ICreditCardService
    {
        IResult Add(CreateCreditCardDto createCreditCardDto);
        IResult Update(CreditCard model);
        IResult Delete(ObjectId id);
        IDataResult<CreditCard> Get(ObjectId id);
        IDataResult<CreditCard> GetByCardNumber(PaymentDto paymentDto);
        IDataResult<List<CreditCard>> GetAll();
    }
}
