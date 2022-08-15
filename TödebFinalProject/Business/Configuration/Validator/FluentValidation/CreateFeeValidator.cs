using Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class CreateFeeValidator : AbstractValidator<CreateFeeDto>
    {
        public CreateFeeValidator()
        {
            RuleFor(b => b.ApartmentId).GreaterThan(0);
            RuleFor(b => b.Amount).GreaterThan(0);
        }
    }
}
