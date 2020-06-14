using ExpressiveAnnotations.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashHomeBuyer.Models
{
    public class Contact
    {
        public int ContactID { get; set; }

        [Required(ErrorMessage = "Please Enter Property Address")]
        public string ContactAddress { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [AssertThat("IsEmail(ContactEmail)")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [AssertThat("IsPhone(ContactPhone)")]
        public string ContactPhone { get; set; }

        public DateTime ContactDate { get; set; }
        public string Notes { get; set; }
    }
}
