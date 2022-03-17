using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto,bool>> filter)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands
                             on c.BrandId equals b.BrandId
                             join cr in context.Colors
                             on c.ColorId equals cr.ColorId                                                         
                             select new CarDetailDto
                             {
                                 CarId=c.Id,
                                 BrandName = b.BrandName,                                 
                                 CarName = c.CarName,
                                 ColorName = cr.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear,
                                 Image = (from image in context.CarImages where c.Id == image.CarId select new CarImageDetailDto { ImagePath = image.ImagePath }).ToList()
                             };

                return filter == null
                    ? result.ToList()
                    : result.Where(filter).ToList();
            }
        }       
    }
}
