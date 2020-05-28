using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressiveAnnotations.Attributes;

namespace CarDealership.Models.Tables
{
    public class ContactUsRequest
    {
        public int ContactUsID { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string ContactName { get; set; }

        public string ContactEmail { get; set; }

        [RequiredIf("ContactEmail == null", ErrorMessage = "Must provide either a phone or email address")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Please enter a message")]
        public string ContactMessage { get; set; }
    }
}
