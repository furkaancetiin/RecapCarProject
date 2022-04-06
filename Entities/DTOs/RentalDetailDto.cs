using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RentalDetailDto:IDto
    {        
        public int CarId { get; set; }       
        public string CarName { get; set; }
        public decimal DailyPrice { get; set; }
        public string ImagePath { get; set; }       
       
    }
}
