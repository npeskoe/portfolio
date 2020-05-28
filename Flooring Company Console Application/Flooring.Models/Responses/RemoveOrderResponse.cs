using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Responses
{
    public class RemoveOrderResponse : Response
    {
        public Order Order { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderNumber { get; set; }
    }
}
