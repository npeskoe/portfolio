using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealership.UI.Models
{
    public class PurchaseSalesInfoViewModel
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
        public int SalesID { get; set; }

        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [AssertThat("IsPhone(CustomerPhone)", ErrorMessage = "Please enter a valid phone number")]
        public string CustomerPhone { get; set; }

        [RequiredIf("CustomerPhone == null", ErrorMessage = "Must provide either a phone or email address")]
        [AssertThat("IsEmail(CustomerEmail)", ErrorMessage = "Please enter a valid email address")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Customer street is required")]
        public string CustomerStreet1 { get; set; }
        public string CustomerStreet2 { get; set; }

        [Required(ErrorMessage = "Customer city is required")]
        public string CustomerCity { get; set; }

        [Required(ErrorMessage = "Customer state is required")]
        public string CustomerState { get; set; }

        [Required(ErrorMessage = "Customer zip is required")]
        [AssertThat("Length(CustomerZip) == 5", ErrorMessage = "Zip code must be a five digit number")]
        [AssertThat("IsNumber(CustomerZip)", ErrorMessage = "Zip code must be a five digit number")]

        public string CustomerZip { get; set; }

        [Required(ErrorMessage = "Purchase price is required")]
        [AssertThat("PurchasePrice != 0", ErrorMessage = "Purchase price cannot be $0")]
        public decimal PurchasePrice { get; set; }

        public int PurchaseTypeID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Id { get; set; }
    }
}