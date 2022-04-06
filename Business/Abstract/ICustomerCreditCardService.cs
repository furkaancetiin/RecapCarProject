using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerCreditCardService
    {
        IResult SaveCustomerCreditCard(CustomerCreditCardModel customerCreditCardModel);
        IDataResult<List<CreditCard>> GetCustomerCreditCardsById(int customerId);

        IResult DeleteCustomerCreditCard(CustomerCreditCard customerCreditCard);
    }
}
