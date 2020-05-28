using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Queries
{
    public class CarMakeItem
    {
        public int MakeID { get; set; }
        public string MakeName { get; set; }
        public DateTime DateAdded { get; set; }
        public string Email { get; set; }
    }
}
