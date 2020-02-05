﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class Products
    {
        public string ProductType { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }
    }
}
