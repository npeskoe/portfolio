using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Queries
{
    public class FeaturedVehicles
    {
        public int VehicleID { get; set; }
        public string ImageFileName { get; set; }
        public int YearBuilt { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public decimal SalesPrice { get; set; }
    }
}
