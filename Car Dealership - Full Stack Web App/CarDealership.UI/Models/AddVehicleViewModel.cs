using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Models
{
    public class AddVehicleViewModel
    {
        public SelectList CarMakeNames { get; set; }
        public string CarMakeName { get; set; }

        [Required(ErrorMessage = "Car model is required")]
        public string ModelName { get; set; }

        public int SalesTypeID { get; set; }
        public int BodyTypeID { get; set; }

        [Required(ErrorMessage = "Year is required" )]
        [AssertThat("YearBuilt >= 2000", ErrorMessage = "Year must be on or after 2000")]
        public int YearBuilt { get; set; }

        public int TransmissionID { get; set; }
        public int ExtColorID { get; set; }
        public int IntColorID { get; set; }

        [Required(ErrorMessage = "Mileage cannot be blank")]
        public int Mileage { get; set; }

        [Required(ErrorMessage = "Vin number cannot be blank")]
        public string VINNumber { get; set; }

        [Required(ErrorMessage = "Mrsp is required")]
        [AssertThat("MSRP != 0", ErrorMessage = "MSRP cannot be $0")]
        public decimal MSRP { get; set; }

        [Required(ErrorMessage = "Sales price is required")]
        [AssertThat("SalesPrice != 0", ErrorMessage = "Sales price cannot be $0")]
        public decimal SalesPrice { get; set; }

        [Required(ErrorMessage = "Vehicle description cannot be blank")]
        public string VehicleDescription { get; set; }

        public string ImageFileName { get; set; }
        
        [Required(ErrorMessage = "Please upload a photo")]
        public HttpPostedFileBase UploadedFile { get; set; }
    }
}
