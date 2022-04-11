using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class FindexPoint:IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int FindexScore { get; set; }
    }
}
