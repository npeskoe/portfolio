using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class SalesInformation
    {
        public int SalesID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerStreet1 { get; set; }
        public string CustomerStreet2 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZip { get; set; }
        public decimal PurchasePrice { get; set; }
        public int PurchaseTypeID { get; set; }
        public int VehicleID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}
