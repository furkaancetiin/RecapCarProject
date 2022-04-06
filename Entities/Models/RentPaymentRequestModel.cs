using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class RentPaymentRequestModel
    {
        public string CardNumber { get; set; }
        public string ExpireMonthAndYear { get; set; }        
        public string Cvc { get; set; }
        public string CardHolderFullName { get; set; }
        public int CustomerId { get; set; }
        public Rental[] Rentals { get; set; }
        public decimal Amount { get; set; }
    }
}
