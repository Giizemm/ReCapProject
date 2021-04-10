using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IRentalService
    {
        IResult Add(Rental rental);
        IResult CheckReturnDate(int carId);

        IDataResult<decimal> GetTotalAmount(int id);
        //IDataResult<Rental> Add(RentalWithDetailDto rentalWithDetailDto);
        IDataResult<List<Rental>> GetAll();
        IDataResult<List<RentalWithDetailDto>> GetAllWithDetail();
        IDataResult<Rental> GetCarByRentalCarId(int carId);

        IResult CheckDate(int carId, DateTime rentDate, DateTime returnDate);
    }
}
