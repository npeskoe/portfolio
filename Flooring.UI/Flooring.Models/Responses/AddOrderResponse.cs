using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Responses
{
    public class AddOrderResponse : Response
    {
        public Order Order { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string StateName { get; set; }
        public string ProductType { get; set; }
        public decimal Area { get; set; }
    }
}
