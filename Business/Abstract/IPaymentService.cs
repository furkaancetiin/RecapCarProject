using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IDataResult<Payment> GetById(int paymentId);
        IDataResult<List<Payment>> GetAll();         
        IResult Update(Payment payment);
        IResult Delete(Payment payment);
        IDataResult<int> Pay(CreditCard creditCard, int customerId, decimal amount);
    }
}
