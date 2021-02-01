using Business.Concrete;
using System;
using System.Linq;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;

namespace ConsolUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine("Model : " + car.ModelYear + " Açıklama : " + car.Description + " " + " Fiyat :" + car.DailyPrice);
            }



        }
    }
}
