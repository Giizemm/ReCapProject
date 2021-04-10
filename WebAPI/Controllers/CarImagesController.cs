using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
        private ICarImageService _carImageService;
        private IWebHostEnvironment _webHostEnvironment;

        public CarImagesController(ICarImageService carImageService, IWebHostEnvironment webHostEnvironment)
        {
            _carImageService = carImageService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _carImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        //public IActionResult AddAsync([FromForm(Name = ("Image"))] IFormFile file, [FromForm] CarImage carImage)
        //{
        //    var result = _carImageService.Add(file, carImage);

        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }

        //    return BadRequest(result);
        //}

        [HttpGet("getImagesByCarId")]
        public IActionResult GetImagesById(int id)
        {
            var result = _carImageService.GetByImagesCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm(Name = "Image")] IFormFile file, [FromForm] CarImage carImage)
        {

            if (file != null && file.Length > 0)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file.FileName);
                var newFileName = Guid.NewGuid() + fileInfo.Extension;
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "images");


                using (var stream = System.IO.File.Create(Path.Combine(filePath, newFileName)))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                }

                carImage.ImagePath = Path.Combine("uploads", "images", newFileName);
                carImage.Date = DateTime.Now;
            }
            //var carimage = new CarImage { CarId = carImage.CarId, ImagePath = carImage.ImagePath, Date = DateTime.Now };
            // var result = _carImageService.Add(file, carImage);

            var result = _carImageService.Add(carImage);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(CarImage carImage)
        {
            var result = _carImageService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm(Name = ("Image"))] IFormFile file, [FromForm] CarImage carImage)
        {
            var result = _carImageService.Update(file, carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
