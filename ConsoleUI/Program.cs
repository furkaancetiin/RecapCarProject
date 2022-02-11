using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Car car1 = new Car
            {
                BrandId = 1,
                CarName = "Opel",
                ColorId = 2,
                DailyPrice = 300,
                Description = "Maximum Sürat = 270 km / saat",
                ModelYear = 2021
            };

            Car car2 = new Car
            {
                BrandId = 2,
                CarName = "Nissan",
                ColorId = 2,
                DailyPrice = 200,
                Description = "Maximum Sürat = 190 km / saat",
                ModelYear = 2019
            };

            ICarService carService = new CarManager(new EfCarDal());
            //carService.Add(car1);
            //carService.Add(car2);
            //carService.Update(new Car {
            //    CarId=1,
            //    BrandId = 1,
            //    CarName = "Ferrari",
            //    ColorId = 2,
            //    DailyPrice = 300,
            //    Description = "Maximum Sürat = 270 km / saat",
            //    ModelYear = 2021
            //});

            //carService.Delete(new Car
            //{
            //    CarId = 2,
            //    BrandId = 2,
            //    CarName = "Nissan",
            //    ColorId = 2,
            //    DailyPrice = 200,
            //    Description = "Maximum Sürat = 190 km / saat",
            //    ModelYear = 2019
            //});

            //foreach (var car in carService.GetAll())
            //{
            //    Console.WriteLine(car.CarName);
            //}

            //foreach (var car in carService.GetCarsByColorId(2))
            //{
            //    Console.WriteLine(car.CarName);
            //}

            //foreach (var car in carService.GetCarsByBrandId(2))
            //{
            //    Console.WriteLine(car.CarName);
            //}

            Console.WriteLine(carService.GetById(1).CarName);

            Color color1 = new Color
            {                
                ColorName = "Kırmızı"
            };

            Color color2 = new Color
            {
                ColorName = "Yeşil"
            };

            Color color3 = new Color
            {
                ColorName = "Mavi"
            };

            IColorService colorService = new ColorManager(new EfColorDal());
            //colorService.Add(color1);
            //colorService.Add(color2);
            //colorService.Add(color3);

            //colorService.Delete(new Color { ColorId = 3, ColorName = "Mavi" });
            //colorService.Update(new Color { ColorId = 2, ColorName = "Mavi" });

            //foreach (var color in colorService.GetAll())
            //{
            //    Console.WriteLine(color.ColorName);
            //}                       

            IBrandService brandService = new BrandManager(new EfBrandDal());
            Brand brand1 = new Brand
            {
                BrandName = "F"
            };

            Brand brand2 = new Brand
            {
                BrandName = "T"
            };

            Brand brand3 = new Brand
            {
                BrandName = "M"
            };

            //brandService.Add(brand1);
            //brandService.Add(brand2);
            //brandService.Add(brand3);

            //brandService.Delete(new Brand { BrandId = 3, BrandName = "M" });
            //brandService.Update(new Brand { BrandId = 2, BrandName = "Z" });

            //foreach (var brand in brandService.GetAll())
            //{
            //    Console.WriteLine(brand.BrandName);
            //}

            //foreach (var car in carService.GetCarDetails())
            //{
            //    Console.WriteLine(car.BrandName + " "+ car.CarName + " " + car.ColorName);
            //}            

        }
    }
}
