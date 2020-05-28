using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{

    public class FeaturedSpecialsViewModel
    {
        public int SpecialID { get; set; }
        public string SpecialName { get; set; }
        public string SpecialDescription { get; set; }
        public int VehicleID { get; set; }
        public string ImageFileName { get; set; }
        public int YearBuilt { get; set; }
        public string MakeName { get; set; }
        public string ModelName { get; set; }
        public decimal SalesPrice { get; set; }
    }
}