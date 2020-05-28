using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Queries
{
    public class CarModelItem
    {
        public int MakeID { get; set; }
        public string MakeName { get; set; }
        public int ModelID { get; set; }        
        public string ModelName { get; set; }
        public DateTime DateAdded { get; set; }
        public string Email { get; set; }
    }
}
