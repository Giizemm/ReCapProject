using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalWithDetailDto> GetAllWithDetail(Expression<Func<RentalWithDetailDto, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from r in context.Rentals
                    join c in context.Cars on r.CarId equals c.Id
                    join b in context.Brands on c.BrandId equals b.BrandId
                    join cu in context.Customers on r.CustomerId equals cu.Id
                    join u in context.Users on cu.UserId equals u.Id
                    select new RentalWithDetailDto
                    {
                        Id = r.Id,
                        CarId = r.CarId,
                        RentDate = r.RentDate,
                        ReturnDate = r.ReturnDate,
                        BrandName = b.Name,
                        CustomerId = r.CustomerId,
                       CustomerFirstAndLastName = u.FirstName + ' ' + u.LastName
                    };
                return result.ToList();
            }
        }
    }
}
