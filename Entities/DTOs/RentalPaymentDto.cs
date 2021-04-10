using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Entities.Concrete;

namespace Entities.DTOs
{
   public class RentalPaymentDto:IDto
    {
        public Rental Rental { get; set; }
        public CreditCard CreditCard { get; set; }

    }
}
