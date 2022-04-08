using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        ICreditCardService _creditCardService;
        IPaymentService _paymentService;        

        public RentalManager(IRentalDal rentalDal, ICreditCardService creditCardService, IPaymentService paymentService)
        {
            _rentalDal = rentalDal;
            _creditCardService = creditCardService;
            _paymentService = paymentService;
            
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
           
            _rentalDal.Add(rental);
            return new SuccessResult();
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int rentalId)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == rentalId));
        }

        public IDataResult<Rental> GetRentalDeliveryById(int carId)
        {
            var getRentalsById = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate>=DateTime.Now);

            foreach (var rental in getRentalsById)
            {
                if (rental == null)
                {
                    return new ErrorDataResult<Rental>();
                }

                if (rental.RentDate <= DateTime.Now && rental.ReturnDate >= DateTime.Now)
                {
                    return new ErrorDataResult<Rental>(Messages.CarNotAvailable);
                }                
            }

            return new SuccessDataResult<Rental>();
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetailsById(int id)
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(c => c.CarId == id));
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IDataResult<int> Rent(RentPaymentRequestModel rentPaymentRequest)
        {
            var creditCardResult = _creditCardService.Get(rentPaymentRequest.CardNumber, rentPaymentRequest.ExpireMonthAndYear, rentPaymentRequest.Cvc, rentPaymentRequest.CardHolderFullName.ToUpper());

            if (creditCardResult == null)
            {
                return new ErrorDataResult<int>(Messages.CreditCardNotValid);
            }            

            var creditCard = creditCardResult.Data;

            var paymentResult = _paymentService.Pay(creditCard, rentPaymentRequest.CustomerId, rentPaymentRequest.Amount);

            foreach (var rental in rentPaymentRequest.Rentals)
            {
                rental.PaymentId = paymentResult.Data;

                var getRentalsById = _rentalDal.GetAll(r => r.CarId == rental.CarId && r.ReturnDate >= DateTime.Now);

                foreach (var rentalDb in getRentalsById)
                {
                    if (rentalDb == null)
                    {
                        return new ErrorDataResult<int>();
                        
                    }

                    if (rentalDb.RentDate <= rental.RentDate && rentalDb.ReturnDate <= rental.ReturnDate)
                    {
                        return new ErrorDataResult<int>(Messages.CarIsNotAvailablePaymentScreen);
                    }                    
                }

                Add(rental);
            }
            
            return new SuccessDataResult<int>(paymentResult.Data, Messages.RentalSuccessful);

        }
       
    }
}

