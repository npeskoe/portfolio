using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Queries
{
    public class VehicleInventoryReport
    {
        public int VehicleID { get; set; }
        public int SalesTypeID { get; set; }
        public int YearBuilt { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public int Count { get; set; }
        public decimal StockValue { get; set; }
    }
}
