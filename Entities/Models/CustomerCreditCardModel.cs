using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class CustomerCreditCardModel
    {
        public int CustomerId { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
