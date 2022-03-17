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

            ICarService carService = new CarManager(new EfCarDal());
            IColorService colorService = new ColorManager(new EfColorDal());
            IBrandService brandService = new BrandManager(new EfBrandDal());            
            ICustomerService customerService = new CustomerManager(new EfCustomerDal());
            IRentalService rentalService = new RentalManager(new EfRentalDal());
            //CarAdd(carService);
            //CarUpdate(carService);
            //CarDelete(carService);
            //CarGetAll(carService);
            //GetCarsByColorIdTest(carService);
            //GetCarsByBrandIdTest(carService);
            //CarGetById(carService);
            //GetCarDetailsTest(carService);
            //ColorAdd(colorService);
            //ColorDelete(colorService);
            //ColorUpdate(colorService);
            //ColorGetAll(colorService);
            //BrandAdd(brandService);
            //BrandDelete(brandService);
            //BrandUpdate(brandService);
            //BrandGetAll(brandService);            
            //UserAdd(userService);
            //CustomerAdd(customerService);
            //RentalAdd(rentalService);

        }

        private static void RentalAdd(IRentalService rentalService)
        {
            Rental rental1 = new Rental { CarId = 1, CustomerId = 1, RentDate = new DateTime(2022, 02, 05) };
            rentalService.Add(rental1);
        }

        private static void CustomerAdd(ICustomerService customerService)
        {
            Customer customer1 = new Customer
            {
                CompanyName = "Anfa",
                UserId = 1
            };

            Customer customer2 = new Customer
            {
                CompanyName = "İstiklal Rezidans",
                UserId = 2
            };

            Customer customer3 = new Customer
            {
                CompanyName = "Lazzoni",
                UserId = 3
            };

            customerService.Add(customer1);
        }
        

            
        

        private static void GetCarDetailsTest(ICarService carService)
        {
            foreach (var car in carService.GetCarDetails().Data)
            {
                Console.WriteLine(car.BrandName + " " + car.CarName + " " + car.ColorName);
            }
        }

        private static void BrandGetAll(IBrandService brandService)
        {
            foreach (var brand in brandService.GetAll().Data)
            {
                Console.WriteLine(brand.BrandName);
            }
        }

        private static void BrandUpdate(IBrandService brandService)
        {
            brandService.Update(new Brand { BrandId = 2, BrandName = "Z" });
        }

        private static void BrandDelete(IBrandService brandService)
        {
            brandService.Delete(new Brand { BrandId = 3, BrandName = "M" });
        }

        private static void BrandAdd(IBrandService brandService)
        {
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

            brandService.Add(brand1);
        }

        private static void ColorGetAll(IColorService colorService)
        {
            foreach (var color in colorService.GetAll().Data)
            {
                Console.WriteLine(color.ColorName);
            }
        }

        private static void ColorUpdate(IColorService colorService)
        {
            colorService.Update(new Color { ColorId = 2, ColorName = "Mavi" });
        }

        private static void ColorDelete(IColorService colorService)
        {
            colorService.Delete(new Color { ColorId = 3, ColorName = "Mavi" });
        }

        private static void ColorAdd(IColorService colorService)
        {
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


            colorService.Add(color1);
        }

        private static void CarGetById(ICarService carService)
        {
            Console.WriteLine(carService.GetById(1).Data.CarName);
        }

        private static void GetCarsByBrandIdTest(ICarService carService)
        {
            foreach (var car in carService.GetCarsByBrandId(2).Data)
            {
                Console.WriteLine(car.CarName);
            }
        }

        private static void GetCarsByColorIdTest(ICarService carService)
        {
            foreach (var car in carService.GetCarsByColorId(2).Data)
            {
                Console.WriteLine(car.CarName);
            }
        }

        private static void CarGetAll(ICarService carService)
        {
            foreach (var car in carService.GetAll().Data)
            {
                Console.WriteLine(car.CarName);
            }
        }

        private static void CarDelete(ICarService carService)
        {
            carService.Delete(new Car
            {
                Id = 2,
                BrandId = 2,
                CarName = "Nissan",
                ColorId = 2,
                DailyPrice = 200,
                Description = "Maximum Sürat = 190 km / saat",
                ModelYear = 2019
            });
        }

        private static void CarUpdate(ICarService carService)
        {
            carService.Update(new Car
            {
                Id = 1,
                BrandId = 1,
                CarName = "Ferrari",
                ColorId = 2,
                DailyPrice = 300,
                Description = "Maximum Sürat = 270 km / saat",
                ModelYear = 2021
            });
        }

        private static ICarService CarAdd(ICarService carService)
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

            
            carService.Add(car1);
            return carService;
        }
    }
}
