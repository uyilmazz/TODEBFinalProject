using DTO.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class CreatePaidTypeValidator : AbstractValidator<CreatePaidTypeDto>
    {
        public CreatePaidTypeValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
