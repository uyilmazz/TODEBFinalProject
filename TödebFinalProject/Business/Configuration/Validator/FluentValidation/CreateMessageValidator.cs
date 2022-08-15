using Dto.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Configuration.Validator.FluentValidation
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageDto>
    {
        public CreateMessageValidator()
        {
            RuleFor(m => m.Content).NotEmpty();
            RuleFor(m => m.Content).MinimumLength(10);
            RuleFor(m => m.Subject).NotEmpty();
            RuleFor(m => m.Subject).MinimumLength(3);
        }
    }
}
