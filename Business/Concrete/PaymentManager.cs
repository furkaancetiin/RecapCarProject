using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        
        public IDataResult<int> Pay(CreditCard creditCard, int customerId, decimal amount)
        {
            DateTime paymentDate = DateTime.Now;
            _paymentDal.Add(new Payment
            {
                CustomerId = customerId,
                CreditCardId = creditCard.Id,
                Amount = amount,
                PaymentDate = paymentDate
            });
            var paymentId = _paymentDal.Get(p => p.CustomerId == customerId && p.Amount == amount && p.CreditCardId == creditCard.Id && (p.PaymentDate.Date == paymentDate.Date && p.PaymentDate.Hour == paymentDate.Hour && p.PaymentDate.Second == paymentDate.Second)).Id;
            return new SuccessDataResult<int>(paymentId, Messages.PaymentSuccessful);
        }

        public IResult Delete(Payment payment)
        {
            _paymentDal.Delete(payment);
            return new SuccessResult();
        }

        public IDataResult<List<Payment>> GetAll()
        {
            return new SuccessDataResult<List<Payment>>(_paymentDal.GetAll());
        }

        public IDataResult<Payment> GetById(int paymentId)
        {
            return new SuccessDataResult<Payment>(_paymentDal.Get(p => p.Id == paymentId));
        }

        public IResult Update(Payment payment)
        {
            _paymentDal.Update(payment);
            return new SuccessResult();
        }
    }
}
