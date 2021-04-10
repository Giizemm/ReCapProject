using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Castle.Core.Internal;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {


        public List<CarWithBrandAndColorDto> GetAllWithDetail(Expression<Func<CarWithBrandAndColorDto, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {

                //var result = from c in context.Cars
                //             join b in context.Brands on c.BrandId equals b.BrandId
                //             join co in context.Colors on c.ColorId equals co.ColorId
                //             join carImage in context.CarImages on c.Id equals carImage.CarId into temp
                //             from t in temp.DefaultIfEmpty()
                //             select new CarWithBrandAndColorDto
                //             {
                //                 Id = c.Id,
                //                 ImagePath = t.ImagePath,
                //                 ModelYear = c.ModelYear,
                //                 DailyPrice = c.DailyPrice,
                //                 Description = c.Description,
                //                 BrandId = c.BrandId,
                //                 Name = b.Name,
                //                 ColorId = c.ColorId,
                //                 ColorName = co.ColorName
                //             };

                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join co in context.Colors on c.ColorId equals co.ColorId
                             select new CarWithBrandAndColorDto
                             {
                                 Id = c.Id,
                                 CarImages = (from i in context.CarImages where i.CarId == c.Id select i).ToList(),
                                 ModelYear = c.ModelYear,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 BrandId = c.BrandId,
                                 Name = b.Name,
                                 ColorId = c.ColorId,
                                 ColorName = co.ColorName
                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public List<CarDetailDto> GetCarDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join co in context.Colors on c.ColorId equals co.ColorId
                             select new CarDetailDto
                             {
                                 Id = c.Id,
                                 Name = b.Name,
                                 ColorName = co.ColorName,
                                 DailyPrice = c.DailyPrice,
                                 Description = c.Description,
                                 ModelYear = c.ModelYear
                             };
                return result.ToList();
            }
        }
    }
}
