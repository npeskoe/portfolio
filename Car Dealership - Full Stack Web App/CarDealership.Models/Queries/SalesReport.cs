using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Queries
{
    public class SalesReport
    {
        public string UserName { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalVehicles { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
