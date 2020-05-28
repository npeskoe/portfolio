using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Responses
{
    public class EditOrderResponse : Response
    {
        public Order Order { get; set; }
        public string NewCustomerName { get; set; }
        public string NewStateName { get; set; }
        public string NewProductType { get; set; }
        public decimal NewArea { get; set; }
    }
}
