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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDal _paymentDal;
        private readonly IMapper _mapper;
        private readonly ICreditCardService _creditCardService;
        private readonly IPaidTypeService _paidTypeService;

        public PaymentService(IPaymentDal paymentDal, IMapper mapper, ICreditCardService creditCardService, IPaidTypeService paidTypeService)
        {
            _paymentDal = paymentDal;
            _mapper = mapper;
            _creditCardService = creditCardService;
            _paidTypeService = paidTypeService;
        }

        public IResult Add(PaymentDto paymentDto)
        {
            var validator = new CreatePaymentValidator();
            var valid = validator.Validate(paymentDto);

            if (!valid.IsValid)
            {
                var message = string.Join(",", valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(message);
            }
            var paidType = _paidTypeService.GetByName(paymentDto.PaidTypeName);
            var payment = _paymentDal.Get(p => p.PaidId == paymentDto.PaidId && p.PaidTypeId == paidType.Data.Id);
            if (payment is not null)
                return new ErrorResult(ResultMessage.AlreadyPaid);

            var cardResult = _creditCardService.GetByCardNumber(paymentDto);
            if (!cardResult.Success)
                return new ErrorResult(ResultMessage.CreditCardNotFound); 
            
           
            if(cardResult.Data.Balance < paymentDto.Amount)
                return new ErrorResult(ResultMessage.InsfufficientBalance);

            cardResult.Data.Balance -= paymentDto.Amount;
            var payResult =  _creditCardService.Update(cardResult.Data);
            if (!payResult.Success)
                return new ErrorResult(ResultMessage.ErrorDuringPayment);

            var newPayment = new Payment() {Amount = paymentDto.Amount,CreditCardId = cardResult.Data.Id,PaidId = paymentDto.PaidId,PaidTypeId = paidType != null ? paidType.Data.Id : null};
            _paymentDal.Add(newPayment);
            return new SuccessResult(ResultMessage.PaymentCompleted);
        }

        public IResult Delete(ObjectId id)
        {
            var payment = _paymentDal.Get(p => p.Id == id);
            if(payment is null)
            {
                return new ErrorResult(ResultMessage.PaymentNotFound);
            }
            _paymentDal.Delete(id);
            return new SuccessResult(ResultMessage.PaymentDeleted);
        }

        public IDataResult<Payment> Get(ObjectId id)
        {
            var payment = _paymentDal.Get(p => p.Id == id);
            if (payment is null)
            {
                return new ErrorDataResult<Payment>(ResultMessage.PaymentNotFound);
            }
            return new SuccessDataResult<Payment>(payment);
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll().ToList());
        }

        public IResult Update(Payment model)
        {
            var payment = _paymentDal.Get(p => p.Id == model.Id);
            if (payment is null)
            {
                return new ErrorResult(ResultMessage.PaymentNotFound);
            }
            _paymentDal.Update(model);
            return new SuccessResult(ResultMessage.PaymentUpdated);
        }
    }
}
