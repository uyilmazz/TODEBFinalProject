using Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class UpdatePersonValidator : AbstractValidator<UpdatePersonDto>
    {
        public UpdatePersonValidator()
        {
            RuleFor(Person => Person.Id).GreaterThan(0);
            RuleFor(Person => Person.FirstName).NotEmpty();
            RuleFor(Person => Person.FirstName).MinimumLength(3);
            RuleFor(Person => Person.LastName).NotEmpty();
            RuleFor(Person => Person.LastName).MinimumLength(3);
            RuleFor(Person => Person.TCNo).NotEmpty();
            RuleFor(Person => Person.TCNo).Length(11);
            RuleFor(Person => Person.Email).NotEmpty();
            RuleFor(Person => Person.Email).EmailAddress();
        }
    }
}
