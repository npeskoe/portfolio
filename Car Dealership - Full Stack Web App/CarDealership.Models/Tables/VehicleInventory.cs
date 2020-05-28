using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class VehicleInventory
    {
        public int VehicleID { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public int SalesTypeID { get; set; }
        public int BodyTypeID { get; set; }
        public int YearBuilt { get; set; }
        public int TransmissionID { get; set; }
        public int ExtColorID { get; set; }
        public int IntColorID { get; set; }
        public int Mileage { get; set; }
        public string VINNumber { get; set; }
        public decimal MSRP { get; set; }
        public decimal SalesPrice { get; set; }
        public string VehicleDescription { get; set; }
        public bool? IsFeaturedVehicle { get; set; }
        public string ImageFileName { get; set; }
    }
}
