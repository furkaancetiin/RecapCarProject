using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckIfCountOfImage(carImage.CarId));
            if (result!=null)
            {
                return result;
            }

            var root = PathConstants.ImagesPath;

            carImage.ImagePath = FileHelper.Upload(file,root);
            _carImageDal.Add(carImage);
            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            FileHelper.Delete(PathConstants.ImagesPath + carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.Id == id));
        }

        public IDataResult<List<CarImage>> GetCarImagesById(int id)
        {            
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c=>c.CarId==id));
        }

        public IResult Update(IFormFile file,CarImage carImage)
        {

            BusinessRules.Run(CheckIfCountOfImage(carImage.CarId));

            var filePath = PathConstants.ImagesPath + carImage.ImagePath;
            var root = PathConstants.ImagesPath;

            FileHelper.Update(file,filePath,root);
            _carImageDal.Update(carImage);
            return new SuccessResult();            
        }

        private IResult CheckIfCountOfImage(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id).Count;
            if (result>4)
            {
                return new ErrorResult(Messages.CountOfImageError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfImageOfCar(int id)
        {
            var result = _carImageDal.GetAll(c => c.CarId == id).Any();
            if (!result)
            {
                return new ErrorResult(Messages.NoCarImageError);
            }
            return new SuccessResult();
        }        
    }
}
