using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CreditCard:IEntity
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonthAndYear { get; set; }        
        public string Cvc { get; set; }
        public string CardHolderFullName { get; set; }        
    }
}
