using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private IRentalService _rentalService;
        private ICreditCardService _creditCardService;

        public RentalsController(IRentalService rentalService, ICreditCardService creditCardService)
        {
            _rentalService = rentalService;
            _creditCardService = creditCardService;
        }

        [HttpGet("getall")]
        public IActionResult Get()
        {
            var result = _rentalService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("CheckReturnDate")]
        public IActionResult CheckReturnDate(int carId)
        {
            var result = _rentalService.CheckReturnDate(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAllWithDetails")]
        public IActionResult GetAllWithDetail()
        {
            var result = _rentalService.GetAllWithDetail();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getCarByRentalCarId")]
        public IActionResult GetCarByRentalCarId(int carId)
        {
            var result = _rentalService.GetCarByRentalCarId(carId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        //[HttpPost("add")]
        //public IActionResult Add(RentalWithDetailDto rentalWithDetailDto)
        //{
        //    var result = _rentalService.Add(rentalWithDetailDto);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //}


        [HttpPost("add")]
        public IActionResult Add(RentalPaymentDto rentalPaymentDto)
        {

            var result = _rentalService.Add(rentalPaymentDto.Rental);
            if (result.Success)
            {
                var paymentResult = _creditCardService.Add(rentalPaymentDto.CreditCard);
                if (!paymentResult.Success)
                {
                    //return Ok(paymentResult);
                    return BadRequest(paymentResult.Message);
                }
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("checkCanRental")]
        public IActionResult CheckCanRental(int carId, DateTime rentDate, DateTime returnDate)
        {
            var result = _rentalService.CheckDate(carId, rentDate, returnDate);
            if (result.Success)
            {
                return Ok(result);
            }

            return Ok(result);
        }



    }
}
