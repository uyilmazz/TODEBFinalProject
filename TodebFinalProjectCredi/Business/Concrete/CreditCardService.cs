using AutoMapper;
using Business.Abstract;
using Business.Configuration.Validator.FluentValidation;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Absract;
using DTO.Concrete;
using Model;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CreditCardService : ICreditCardService
    {
        private readonly ICreditCardDal _creditCardDal;
        private readonly IMapper _mapper;

        public CreditCardService(ICreditCardDal creditCardDal, IMapper mapper)
        {
            _creditCardDal = creditCardDal;
            _mapper = mapper;
        }

        public IResult Add(CreateCreditCardDto createCreditCardDto)
        {
            var validator = new CreateCreditCardDtoValidator();
            var valid = validator.Validate(createCreditCardDto);
            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }
            var creditCard = _creditCardDal.Get(c => c.CardNumber == createCreditCardDto.CardNumber && c.CustomerName == createCreditCardDto.CustomerName);
            if(creditCard is not null)
            {
                return new ErrorResult(ResultMessage.CreditCardAlreayExists);
            }
            creditCard = _mapper.Map<CreditCard>(createCreditCardDto);
            _creditCardDal.Add(creditCard);
            return new SuccessResult(ResultMessage.CreditCardAdded);
        }

        public IResult Update(CreditCard model)
        {
            var creditCard = _creditCardDal.Get(model.Id);
            if(creditCard is null)
            {
                return new ErrorResult(ResultMessage.CreditCardNotFound);
            }
            _creditCardDal.Update(model);
            return new SuccessResult(ResultMessage.CreditCardUpdated);
            
        }

        public IResult Delete(ObjectId id)
        {
            var creditCard = _creditCardDal.Get(id);
            if(creditCard is null)
            {
                return new ErrorResult(ResultMessage.CreditCardNotFound);
            }
            _creditCardDal.Delete(id);
            return new SuccessResult(ResultMessage.CreditCardDeleted);
        }

        public IDataResult<CreditCard> Get(ObjectId id)
        {
            var creditCard = _creditCardDal.Get(id);
            if(creditCard is null)
            {
                return new ErrorDataResult<CreditCard>(ResultMessage.CreditCardNotFound);
            }
            return new SuccessDataResult<CreditCard>(creditCard);
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll().ToList());
        }

        public IDataResult<CreditCard> GetByCardNumber(PaymentDto paymentDto)
        {
            var result = _creditCardDal.Get(c => c.CardNumber == paymentDto.CardNumber && c.Cvc == paymentDto.Cvc &&
            c.ExpireMonth == paymentDto.ExpireMonth && c.ExpireYear == paymentDto.ExpireYear && c.CustomerName == paymentDto.CustomerName);
            if(result is null)
            {
                return new ErrorDataResult<CreditCard>(ResultMessage.CreditCardNotFound);
            }
            return new SuccessDataResult<CreditCard>(result);
        }
    }
}
