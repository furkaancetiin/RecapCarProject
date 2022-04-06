
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CreditCardManager:ICreditCardService
    {
        ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }

        public IDataResult<List<CreditCard>> GetAllCreditCard()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll());
        }

        public IDataResult<CreditCard> GetCreditCardById(int creditCardId)
        {
            _creditCardDal.Get(c => c.Id == creditCardId);
            return new SuccessDataResult<CreditCard>();
        }

        [ValidationAspect(typeof(CreditCardValidator))]
        public IDataResult<CreditCard> Get(string cardNumber, string expireMonthAndYear, string cvc, string cardHolderFullName)
        {
            var creditCard = _creditCardDal.Get(c => c.CardNumber == cardNumber &&
                                           c.ExpireMonthAndYear == expireMonthAndYear &&
                                           c.Cvc == cvc &&
                                           c.CardHolderFullName == cardHolderFullName.ToUpperInvariant()); 

            if (creditCard != null)
            {
                return new SuccessDataResult<CreditCard>(creditCard);
            }
            return new ErrorDataResult<CreditCard>(null, Messages.CreditCardNotValid);
        }    
    }
}
