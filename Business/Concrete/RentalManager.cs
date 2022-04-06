using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
            var result = _rentalDal.Get(r => r.CarId == carId);

            if (result == null)
            {
                return new SuccessDataResult<Rental>();
            }

            if (result.RentDate <= DateTime.Now && result.ReturnDate >= DateTime.Now)
            {
                return new ErrorDataResult<Rental>(Messages.CarNotAvailable);
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

        public IDataResult<int> Rent(RentPaymentRequestModel rentPaymentRequest)
        {
            var creditCardResult = _creditCardService.Get(rentPaymentRequest.CardNumber, rentPaymentRequest.ExpireMonthAndYear, rentPaymentRequest.Cvc, rentPaymentRequest.CardHolderFullName.ToUpper());

            List<Rental> rentals = new List<Rental>();

            var creditCard = creditCardResult.Data;

            var paymentResult = _paymentService.Pay(creditCard, rentPaymentRequest.CustomerId, rentPaymentRequest.Amount);

            foreach (var rental in rentPaymentRequest.Rentals)
            {
                rental.PaymentId = paymentResult.Data;

                Add(rental);

            }

            return new SuccessDataResult<int>(paymentResult.Data, Messages.RentalSuccessful);

        }
    }
}

