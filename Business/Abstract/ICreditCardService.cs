using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IDataResult<CreditCard> Get(string cardNumber, string expireMonthAndYear, string cvc, string cardHolderFullName);
        IDataResult<CreditCard> GetCreditCardById(int creditCardId);
        IDataResult<List<CreditCard>> GetAllCreditCard();
    }
}
