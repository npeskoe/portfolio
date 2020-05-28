using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class SalesReportViewModel
    {
        public SelectList Users { get; set; }
        public string UserName { get; set; }
        public decimal TotalSales { get; set; }
        public int TotalVehicles { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}