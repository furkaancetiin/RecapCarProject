using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>()
            {
                new Car{BrandId=3,ColorId=4,CarName="Opel",DailyPrice=250,Description="Maximum Sürat=205 km/saat",ModelYear=2019},
                new Car{BrandId=4,ColorId=2,CarName="Opel",DailyPrice=300,Description="Maximum Sürat=300 km/saat",ModelYear=2021},
                new Car{BrandId=1,ColorId=3,CarName="Mazda",DailyPrice=135,Description="Maximum Sürat=155 km/saat",ModelYear=2008},
                new Car{BrandId=3,ColorId=4,CarName="Honda",DailyPrice=180,Description="Maximum Sürat=180 km/saat",ModelYear=2012},

            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car deleteFromCarList = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(deleteFromCarList);
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car updateToCarList = _cars.SingleOrDefault(c => c.Id == car.Id);
            updateToCarList.ModelYear = car.ModelYear;
            updateToCarList.ColorId = car.ColorId;
            updateToCarList.Description = car.Description;
            updateToCarList.BrandId = car.BrandId;
            updateToCarList.DailyPrice = car.DailyPrice;

        }
    }
}
