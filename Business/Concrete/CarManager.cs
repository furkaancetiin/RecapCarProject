﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        {

            if (car.CarName.Length < 2)
            {
                return new ErrorResult(Messages.CarNameInvalid);

            }
            else if (car.DailyPrice <= 0)
            {
                return new ErrorResult(Messages.CarPriceInvalid);
            }
            else
            {
                _carDal.Add(car);
                return new SuccessResult();
            }           

        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour==23)
            {
                return new ErrorDataResult<List<Car>>(_carDal.GetAll(), Messages.CarMaintenanceTime);
            }
            else
            {
                return new SuccessDataResult<List<Car>>();
            }
        }

        public IDataResult<Car> GetById(int carId)
        {
           return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
            
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(b => b.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IResult Update(Car car)
        {
            if (car.CarName.Length < 2)
            {
                return new ErrorResult(Messages.CarNameInvalid);

            }
            else if (car.DailyPrice <= 0)
            {
                return new ErrorResult(Messages.CarPriceInvalid);
            }
            else
            {
                _carDal.Update(car);
                return new SuccessResult();
            }
        }
    }
}
