using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Queries
{
    public class VehicleItem
    {
        public int VehicleID { get; set; }
        public int SalesTypeID { get; set; }
        public int YearBuilt { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public string ImageFileName { get; set; }
        public string BodyTypeName { get; set; }
        public string TransmissionTypeName { get; set; }
        public string ExtColorName { get; set; }
        public string IntColorName { get; set; }
        public int Mileage { get; set; }
        public string VINNumber { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal MSRP { get; set; }
        public string VehicleDescription { get; set; }
        public bool? IsSold { get; set; }
    }
}
