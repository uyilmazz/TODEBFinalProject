using DTO.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class CreatePaymentValidator : AbstractValidator<PaymentDto>
    {
        public CreatePaymentValidator()
        {
            RuleFor(p => p.CardNumber).NotEmpty();
            RuleFor(p => p.CardNumber).Length(16);
            RuleFor(p => p.PaidId).NotEmpty();
            RuleFor(p => p.PaidId).GreaterThan(0);
            RuleFor(p => p.Amount).NotEmpty();
            RuleFor(p => p.Amount).GreaterThan(0);
            RuleFor(p => p.ExpireMonth).NotEmpty();
            RuleFor(p => p.ExpireMonth).LessThanOrEqualTo(12);
            RuleFor(p => p.ExpireYear).NotEmpty();
            RuleFor(p => p.Cvc).NotEmpty();
            RuleFor(p => p.CustomerName).NotEmpty();

        }
    }
}
