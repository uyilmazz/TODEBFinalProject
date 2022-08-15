using Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class UpdatePersonTypeValidator : AbstractValidator<UpdatePersonTypeDto>
    {
        public UpdatePersonTypeValidator()
        {
            RuleFor(u => u.Id).GreaterThan(0);
            RuleFor(u => u.Name).NotEmpty();
        }
    }
}
