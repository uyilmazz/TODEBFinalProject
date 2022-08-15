using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class UpdateApartmentTypeValidator : AbstractValidator<ApartmentType>
    {
        public UpdateApartmentTypeValidator()
        {

            RuleFor(a => a.Id).GreaterThan(0);
            RuleFor(a => a.Name).NotEmpty();
        }
    }
}
