using Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class CreateBillBulkValidator : AbstractValidator<CreateBillDto>
    {
        public CreateBillBulkValidator()
        {
            RuleFor(b => b.Amount).GreaterThan(0);
        }
    }
}
