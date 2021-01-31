﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id = 1,BrandId = 1,ColorId = 1,DailyPrice = 600,ModelYear = 2020,Description = "Abs, Yolcu Airbag,27 Yaş Üstü,Ehliyet Yaşı 5 ve üzeri"},
                new Car{Id = 2,BrandId = 1,ColorId = 2,DailyPrice = 220,ModelYear = 2015,Description = "Abs, Yolcu Airbag,23 Yaş Üstü,Ehliyet Yaşı 1 ve üzeri"},
                new Car{Id = 3,BrandId = 2,ColorId = 2,DailyPrice = 300,ModelYear = 2017,Description = "Abs, Yolcu Airbag,25 Yaş Üstü,Ehliyet Yaşı 3 ve üzeri"},
                new Car{Id = 4,BrandId = 2,ColorId = 3,DailyPrice = 350,ModelYear = 2019,Description = "Abs, Yolcu Airbag,21 Yaş Üstü,Ehliyet Yaşı 2 ve üzeri"},
                new Car{Id = 5,BrandId = 2,ColorId = 3,DailyPrice = 400,ModelYear = 2019,Description = "Abs, Yolcu Airbag,25 Yaş Üstü,Ehliyet Yaşı 2 ve üzeri"},
                new Car{Id = 6,BrandId = 3,ColorId = 5,DailyPrice = 200,ModelYear = 2015,Description = "Abs, Yolcu Airbag,30 Yaş Üstü,Ehliyet Yaşı 1 ve üzeri"}
        };
        }
        public void Add(Car car)
        {
           _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(x => x.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }


        public List<Car> GetById(int carId)
        {
            return _cars.Where(x => x.Id==carId).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(x => x.Id == car.Id);
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
            
        }
    }
}