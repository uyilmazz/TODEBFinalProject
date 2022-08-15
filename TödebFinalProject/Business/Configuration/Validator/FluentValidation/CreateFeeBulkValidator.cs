using Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class CreateFeeBulkValidator : AbstractValidator<CreateFeeDto>
    {
        public CreateFeeBulkValidator()
        {
            RuleFor(b => b.Amount).GreaterThan(0);
        }
    }
}

