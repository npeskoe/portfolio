using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL.CalculateRules
{
    public class CalculateOrderRules : ICalculateOrder
    {
        public CalculateOrderResponse CalculateOrder(string customerName, string productType, decimal Area, string State, decimal taxRate, decimal costPerSquareFoot,
            decimal LaborCostPerSquareFoot, decimal materialCost, decimal laborCost, decimal tax)
        {
            CalculateOrderResponse response = new CalculateOrderResponse();

            response.CustomerName = customerName;
            response.ProductType = productType;
            response.Area = Area;
            response.State = State;
            response.TaxRate = taxRate;
            response.CostPerSquareFoot = costPerSquareFoot;
            response.LaborCostPerSquareFoot = LaborCostPerSquareFoot;

            response.Success = true;

            response.Message = "Additional Order Details Calculated";

            return response;
        }
    }
}
