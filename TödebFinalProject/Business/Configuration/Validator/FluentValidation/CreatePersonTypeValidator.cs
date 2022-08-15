using Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class CreatePersonTypeValidator : AbstractValidator<CreatePersonTypeDto>
    {
        public CreatePersonTypeValidator()
        {
            RuleFor(u => u.Name).NotEmpty();
         
        }
    }
}
