using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
        IFindexPointService _findexPointService;
        ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICreditCardService creditCardService, IPaymentService paymentService, IFindexPointService findexPointService, ICarService carService)
        {
            _rentalDal = rentalDal;
            _creditCardService = creditCardService;
            _paymentService = paymentService;
            _findexPointService = findexPointService;
            _carService = carService;

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
            var getRentalsById = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate >= DateTime.Now);

            foreach (var rental in getRentalsById)
            {

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
            var creditCardResult = _creditCardService.Get(rentPaymentRequest.CardNumber, rentPaymentRequest.ExpireMonthAndYear, rentPaymentRequest.Cvc, rentPaymentRequest.CardHolderFullName.ToUpper()).Data;

            if (creditCardResult == null)
            {
                return new ErrorDataResult<int>(Messages.CreditCardNotValid);
            }

            var paymentResult = _paymentService.Pay(creditCardResult, rentPaymentRequest.CustomerId, rentPaymentRequest.Amount);

            foreach (var rental in rentPaymentRequest.Rentals)
            {
                rental.PaymentId = paymentResult.Data;

                IResult resultCheck = BusinessRules.Run(CheckIfCarIsRentable(rental),
                    CheckIfTotalAmount(rentPaymentRequest.Rentals, rentPaymentRequest.Amount),
                    CheckIfCustomerFindexPoint(rentPaymentRequest.CustomerId, rentPaymentRequest.FindexScores));

                if (resultCheck != null)
                {
                    return new ErrorDataResult<int>(-1, resultCheck.Message);
                }

                Add(rental);
            }

            return new SuccessDataResult<int>(paymentResult.Data, Messages.RentalSuccessful);

        }

        private IResult CheckIfCustomerFindexPoint(int customerId, SingleFindexPointModel[] singleFindexPointModels)
        {             

            var customerFindexPointResult = _findexPointService.GetFindexPointByCustomerId(customerId).Data;

            if (customerFindexPointResult == null)
            {
                return new ErrorResult(Messages.FindexPointNotAvailable);
            }

            var customerFindexPoint = customerFindexPointResult.FindexScore;

            for (int i = 0; i < singleFindexPointModels.Length; i++)
            {
                
                if (_carService.GetById(singleFindexPointModels[i].CarId).Data.FindexScore != singleFindexPointModels[i].FindexScore)
                {
                    return new ErrorResult(Messages.CarFindexPointNotToPair);
                }
                if (customerFindexPoint < singleFindexPointModels[i].FindexScore)
                {
                    return new ErrorResult(Messages.CustomerFindexPointNotEnough);
                }
            }

            return new SuccessResult();
        }

        private IResult CheckIfCarIsRentable(Rental rental)
        {
            var rentalByIdResults = _rentalDal.GetAll(r => r.CarId == rental.CarId && r.ReturnDate >= DateTime.Now && r.RentDate >= DateTime.Now);

            foreach (var rentalDb in rentalByIdResults)
            {

                if ((rental.RentDate >= rentalDb.RentDate && rental.RentDate <= rentalDb.ReturnDate) ||
                    (rental.ReturnDate >= rentalDb.RentDate && rental.ReturnDate <= rentalDb.ReturnDate) ||
                    (rentalDb.RentDate >= rental.RentDate && rentalDb.RentDate <= rental.ReturnDate) ||
                    (rentalDb.ReturnDate >= rental.RentDate && rentalDb.ReturnDate <= rental.ReturnDate))

                {
                    return new ErrorDataResult<int>(Messages.CarIsNotRentable);

                }
            }
            return new SuccessResult();
        }

        private IResult CheckIfTotalAmount(Rental[] rentals, decimal amount)
        {
            decimal totalAmountDb = 0;

            for (int i = 0; i < rentals.Length; i++)
            {
                var carResult = _carService.GetById(rentals[i].CarId);

                TimeSpan differenceDays = (rentals[i].ReturnDate - rentals[i].RentDate);

                var numberOfDays = differenceDays.Days;

                var carTotalPrice = numberOfDays * carResult.Data.DailyPrice;

                totalAmountDb += carTotalPrice;
            }

            if (totalAmountDb != amount)
            {
                return new ErrorResult(Messages.TotalAmountError);
            }

            return new SuccessResult();

        }
    }
}

