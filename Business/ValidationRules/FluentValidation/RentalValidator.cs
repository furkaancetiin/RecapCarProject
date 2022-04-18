using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.RentDate).GreaterThan(DateTime.Now);
            RuleFor(r => r.ReturnDate).GreaterThan(r => r.RentDate);


        }
    }
}

