using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<RentalDetailDto, bool>> filter=null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars
                             on r.CarId equals c.Id                             
                             join cr in context.CarImages
                             on r.CarId equals cr.CarId
                             select new RentalDetailDto
                             {                                 
                                 CarId=c.Id,                                 
                                 ImagePath=cr.ImagePath,
                                 CarName=c.CarName,
                                 DailyPrice=c.DailyPrice,                                
                             };

                return filter == null
                     ? result.ToList()
                     : result.Where(filter).ToList();
            }
        }
    }
}
