using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class UpdateApartmentBlocValidator : AbstractValidator<ApartmentBloc>
    {
        public UpdateApartmentBlocValidator()
        {
            RuleFor(a => a.Id).GreaterThan(0);
            RuleFor(a => a.Name).NotEmpty();
        }
    }
}
