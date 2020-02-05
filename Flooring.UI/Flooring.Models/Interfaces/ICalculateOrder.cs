using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Interfaces
{
    public interface ICalculateOrder
    {
        CalculateOrderResponse CalculateOrder(string customerName, string productType, decimal Area, string State, decimal taxRate, decimal costPerSquareFoot,
            decimal LaborCostPerSquareFoot, decimal materialCost, decimal laborCost, decimal tax);
    }
}
