using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CustomerCreditCardManager : ICustomerCreditCardService
    {
        ICustomerCreditCardDal _customerCreditCardDal;
        ICreditCardService _creditCardService;

        public CustomerCreditCardManager(ICustomerCreditCardDal customerCreditCardDal, ICreditCardService creditCardService)
        {
            _customerCreditCardDal = customerCreditCardDal;
            _creditCardService = creditCardService;
        }

        public IResult DeleteCustomerCreditCard(CustomerCreditCard customerCreditCard)
        {    
            _customerCreditCardDal.Delete(customerCreditCard);
            return new SuccessResult();
        }


        public IDataResult<List<CreditCard>> GetCustomerCreditCardsById(int customerId)
        {
            var getAllCustomerCreditCard = _customerCreditCardDal.GetAll(c => c.CustomerId == customerId);

            if (getAllCustomerCreditCard == null)
            {
                return new ErrorDataResult<List<CreditCard>>(Messages.CustomerCreditCardIsAvailable);
            }

            var getAllCreditCard = _creditCardService.GetAllCreditCard();

            List<CreditCard> creditCards = new List<CreditCard>();

            for (int i = 0; i < getAllCustomerCreditCard.Count; i++)
            {
                for (int j = 0; j < getAllCreditCard.Data.Count; j++)
                {
                    if (getAllCustomerCreditCard[i].CreditCardId == getAllCreditCard.Data[j].Id)
                    {
                        creditCards.Add(getAllCreditCard.Data[j]);
                    }

                }

            }           

            return new SuccessDataResult<List<CreditCard>>(creditCards);

        }

        public IResult SaveCustomerCreditCard(CustomerCreditCardModel customerCreditCardModel)
        {
            var getCreditCard = _creditCardService.Get(customerCreditCardModel.CreditCard.CardNumber,
                                                    customerCreditCardModel.CreditCard.ExpireMonthAndYear,
                                                    customerCreditCardModel.CreditCard.Cvc,
                                                    customerCreditCardModel.CreditCard.CardHolderFullName.ToUpperInvariant());
            if (!getCreditCard.Success)
            {
                return new ErrorResult(getCreditCard.Message);
            }

            CustomerCreditCard customerCreditCard = new CustomerCreditCard
            {
                CustomerId = customerCreditCardModel.CustomerId,
                CreditCardId = getCreditCard.Data.Id
            };

            var customerCreditCardExist = _customerCreditCardDal.GetAll(c =>
                c.CustomerId == customerCreditCard.CustomerId && c.CreditCardId == customerCreditCard.CreditCardId).Any();

            if (customerCreditCardExist)
            {
                return new ErrorResult(Messages.CustomerCreditCardIsAvailable);
            }

            _customerCreditCardDal.Add(customerCreditCard);

            return new SuccessResult();

        }        
    }
}

