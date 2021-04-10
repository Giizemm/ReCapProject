using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICarService
    {
        IResult Add(Car car);
        IDataResult<List<Car>> GetAll();
        IDataResult<List<CarDetailDto>> GetCarDetails();
        IDataResult<List<CarWithBrandAndColorDto>> GetAllWithDetail();
        
        IResult Update(Car car);
        IResult Delete(Car car);
        IResult AddTransactionalTest(Car car);
        IDataResult<List<CarWithBrandAndColorDto>> GetByBrandId(int brandId);
        IDataResult<List<CarWithBrandAndColorDto>> GetByColorId(int colorId);
        IDataResult<List<CarWithBrandAndColorDto>> GetCarByImageId(int id);
        IDataResult<List<CarWithBrandAndColorDto>> GetBycolorAndBrandId(int colorId,int brandId);
        IDataResult<Car> GetCarById(int carId);
    }
}
