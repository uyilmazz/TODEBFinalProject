
using DTO.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class CreateCreditCardDtoValidator : AbstractValidator<CreateCreditCardDto>
    {
        public CreateCreditCardDtoValidator()
        {
            RuleFor(c => c.CustomerName).NotEmpty();
            RuleFor(c => c.CustomerName).MinimumLength(3);
            RuleFor(c => c.CardNumber).NotEmpty();
            RuleFor(c => c.CardNumber).Length(16);
            RuleFor(c => c.ExpireYear).NotEmpty();
            RuleFor(c => c.ExpireMonth).NotEmpty();
            RuleFor(c => c.ExpireMonth).LessThanOrEqualTo(12);
            RuleFor(c => c.Cvc).NotEmpty();
        }
    }
}
