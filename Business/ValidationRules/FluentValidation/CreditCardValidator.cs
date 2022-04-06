using Business.Constants;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CreditCardValidator:AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {            
            RuleFor(c => c.CardNumber).NotEmpty();
            RuleFor(c => c.CardNumber).Length(16);
            RuleFor(c => c.CardNumber).Must(CheckIfNumberString).WithMessage(Messages.StringMustConsistOfNumbersOnly);
            
            RuleFor(c => c.ExpireMonthAndYear).NotEmpty();
            RuleFor(c => c.ExpireMonthAndYear).Length(5);
            RuleFor(c => c.ExpireMonthAndYear).Must(CheckIfNumberString).WithMessage(Messages.StringMustConsistOfNumbersOnly);           

         
            RuleFor(c => c.Cvc).NotEmpty();
            RuleFor(c => c.Cvc).Length(3);
            RuleFor(c => c.Cvc).Must(CheckIfNumberString).WithMessage(Messages.StringMustConsistOfNumbersOnly);
            
            RuleFor(c => c.CardHolderFullName).NotEmpty();            
            RuleFor(c => c.CardHolderFullName).MaximumLength(50);
        }
        private bool CheckIfNumberString(string input)
        {
            foreach (var chr in input)
            {
                if (!Char.IsNumber(chr))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
