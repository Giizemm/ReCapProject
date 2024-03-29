﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.DTOs
{
    public class CarDetailDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ColorName { get; set; }
        public string Description { get; set; }
        public int DailyPrice { get; set; }
        public int ModelYear { get; set; }
    }
}
