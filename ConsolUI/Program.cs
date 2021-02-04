using Business.Concrete;
using System;
using System.Linq;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsolUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());

            foreach (var car in carManager.GetCarsByBrandId(1))
            {
                Console.WriteLine("Model : " + car.ModelYear + " Açıklama : " + car.Description + " " + " Fiyat :" + car.DailyPrice);
            }

            foreach (var car in carManager.GetCarsByColorId(1))
            {
                Console.WriteLine("Model : " + car.ModelYear + " Açıklama : " + car.Description + " " + " Fiyat :" + car.DailyPrice);
            }

            try
            {
                carManager.Add(new Car() { BrandId = 2, ColorId = 3, DailyPrice = 300, Description = "a", ModelYear = 2019 });


                Console.WriteLine("Araçlar başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {

                carManager.Add(new Car() { BrandId = 5, ColorId = 4, DailyPrice = 150, Description = "Ehliyet yaş sınırı yoktur.", ModelYear = 2000 });

                Console.WriteLine("Araçlar başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }
}
