using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Interfaces
{
    public interface IRemoveOrder
    {
        RemoveOrderResponse RemoveOrder(Order order, DateTime orderDate, int orderNumber); 
    }
}
