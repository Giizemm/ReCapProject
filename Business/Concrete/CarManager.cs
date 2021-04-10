using System;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private ICarDal _carDal;


        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("car.add,admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            _carDal.Add(car);
            return new SuccessResult(Messages.CarAdded);

        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccessResult(Messages.CarUpdated);
        }
        public IDataResult<Car> GetCarById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(x => x.Id == carId));

        }
        

        public IDataResult<List<CarWithBrandAndColorDto>> GetByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarWithBrandAndColorDto>>(CheckIfCarImageIsEmpty(_carDal.GetAllWithDetail(x => x.BrandId == brandId)));
        }

        public IDataResult<List<CarWithBrandAndColorDto>> GetByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarWithBrandAndColorDto>>(CheckIfCarImageIsEmpty(_carDal.GetAllWithDetail(x => x.ColorId == colorId)));
        }

        public IDataResult<List<CarWithBrandAndColorDto>> GetCarByImageId(int id)
        {
            return new SuccessDataResult<List<CarWithBrandAndColorDto>>(CheckIfCarImageIsEmpty(_carDal.GetAllWithDetail(x => x.Id == id)));
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.Listed);
        }

        public IDataResult<List<CarWithBrandAndColorDto>> GetAllWithDetail()
        {
            return new SuccessDataResult<List<CarWithBrandAndColorDto>>(CheckIfCarImageIsEmpty(_carDal.GetAllWithDetail()));
        }



        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new DataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), true, Messages.Listed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(x => x.BrandId == id));
        }


        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(x => x.ColorId == id));
        }
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        private List<CarWithBrandAndColorDto> CheckIfCarImageIsEmpty(List<CarWithBrandAndColorDto> cars)
        {
            string path = "uploads\\images\\default.jpg";
            foreach (var car in cars)
            {
                if (!car.CarImages.Any())
                {
                    car.CarImages.Add(new CarImage() { CarId = car.Id, Date = DateTime.Now, ImagePath = "\\uploads\\images\\default.jpg" });
                }
            }
            return cars;

        }

        public IDataResult<List<CarWithBrandAndColorDto>> GetBycolorAndBrandId(int colorId, int brandId)
        {
            return new SuccessDataResult<List<CarWithBrandAndColorDto>>(CheckIfCarImageIsEmpty(_carDal.GetAllWithDetail(x => x.ColorId == colorId && x.BrandId==brandId)));
        }
    }
}
