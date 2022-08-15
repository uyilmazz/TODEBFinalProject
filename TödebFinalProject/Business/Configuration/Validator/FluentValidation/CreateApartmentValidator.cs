using Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class CreateApartmentValidator : AbstractValidator<CreateApartmentDto>
    {
        public CreateApartmentValidator()
        {
            RuleFor(apartment => apartment.BlocId).GreaterThan(0);
            RuleFor(apartment => apartment.PersonId).GreaterThan(0).When(apartment => apartment.PersonId != null);
            RuleFor(apartment => apartment.TypeId).GreaterThan(0);
            RuleFor(apartment => apartment.ApartmentNumber).GreaterThan(1);
        }
    }
}
