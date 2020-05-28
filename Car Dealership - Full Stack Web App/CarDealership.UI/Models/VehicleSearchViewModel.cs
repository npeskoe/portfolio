using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.UI.Models
{
    public class VehicleSearchViewModel
    {
        public string SearchString { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string MinYear { get; set; }
        public string MaxYear { get; set; }
    }
}