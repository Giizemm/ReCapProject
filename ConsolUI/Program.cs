using Business.Concrete;
using System;
using DataAccess.Concrete.InMemory;

namespace ConsolUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new InMemoryCarDal());

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine("Model : "+car.ModelYear + " Açıklama : "+car.Description+" "+" Fiyat :"+car.DailyPrice);
            }
        }
    }
}
