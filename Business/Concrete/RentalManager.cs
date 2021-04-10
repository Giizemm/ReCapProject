using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;
        private ICreditCardService _creditCardService;
        private ICarService _carService;

        public RentalManager(IRentalDal rentalDal, ICreditCardService creditCardService, ICarService carService)
        {
            _rentalDal = rentalDal;
            _creditCardService = creditCardService;
            _carService = carService;
        }

        public IResult Add(Rental rental)
        {
            if (!CheckDate(rental.CarId, rental.RentDate, rental.ReturnDate).Success)
            {
                return new ErrorResult(Messages.CarNotInStock);
            }


            _rentalDal.Add(rental);
            return new SuccessResult(Messages.CarAdded);
        }

        [CacheRemoveAspect("IRentalService.Get")]

        //public IDataResult<Rental> Add(RentalWithDetailDto rentalWithDetailDto)
        //{
        //    if (!CheckDate(rentalWithDetailDto.CarId, rentalWithDetailDto.RentDate).Success)
        //        return new ErrorDataResult<Rental>(null, Messages.CarNotInStock);

        //    var rentalToAdd = new Rental()
        //    {
        //        CarId = rentalWithDetailDto.CarId,
        //        CustomerId = rentalWithDetailDto.CustomerId,
        //        RentDate = rentalWithDetailDto.RentDate,
        //        ReturnDate = (DateTime)rentalWithDetailDto.ReturnDate
        //    };

        //    _rentalDal.Add(rentalToAdd);


        //    return new SuccessDataResult<Rental>(rentalToAdd, Messages.CarAdded);

        //}

        public IResult CheckReturnDate(int carId)
        {
            var result = _rentalDal.GetAll(x => x.Id == carId);
            if (result == null)
            {
                return new ErrorResult(Messages.BrandNameInvalid);
            }

            return new SuccessResult(Messages.Listed);
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.Listed);
        }

        public IDataResult<List<RentalWithDetailDto>> GetAllWithDetail()
        {
            return new SuccessDataResult<List<RentalWithDetailDto>>(_rentalDal.GetAllWithDetail());
        }

        public IDataResult<Rental> GetCarByRentalCarId(int carId)
        {
            var result = _rentalDal.Get(x => x.Id == carId);


            return new SuccessDataResult<Rental>(result);

        }

        public IDataResult<decimal> GetTotalAmount(int id)
        {
            var rental = _rentalDal.Get(r => r.Id == id);
            var car = _carService.GetCarById(rental.CarId);
            var days = (rental.ReturnDate - rental.RentDate).Days;
            var totalAmount = days * car.Data.DailyPrice;
            return new SuccessDataResult<decimal>(totalAmount, "");
        }

        public IResult CheckDate(int carId, DateTime rentDate, DateTime returnDate)
        {
            var rentals = _rentalDal.GetAll(x => x.CarId == carId);
            foreach (var rental in rentals)
            {
                // kiralanmak istenen başlangıç tarihleri arasında bir kiralama kaydı yapılmışsa hata döndür
                if ((rentDate <= rental.ReturnDate && rentDate >= rental.RentDate) ||
                    (returnDate <= rental.ReturnDate && returnDate >= rental.RentDate))
                {
                    return new ErrorResult();
                }

            }
            return new SuccessResult();

        }


    }
}
