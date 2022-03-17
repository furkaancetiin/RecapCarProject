using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarImageDetailDto:IDto
    {
        public string ImagePath { get; set; }
    }
}
