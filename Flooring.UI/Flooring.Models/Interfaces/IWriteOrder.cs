using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Interfaces
{
    public interface IWriteOrder
    {
        WriteOrderResponse WriteOrder(Order order, DateTime orderDate, int OrderNumber, string customerName, string stateName, decimal taxRate, string productType, decimal Area, decimal CostPerSquareFoot,
            decimal LaborCostPerSquareFoot, decimal MaterialCost, decimal LaborCost, decimal Tax, decimal Total);
    }
}
