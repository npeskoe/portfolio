using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Interfaces
{
    public interface IOrderRepository
    {
        Order AddOrder(Order order, DateTime orderDate, string customerName, string stateName, string productType, decimal area);

        Order CalculateOrder(Order order, string customerName, string stateName, string productType, decimal Area);

        Order WriteOrder(Order order, DateTime orderDate, int orderNumber, string customerName, string stateName, decimal taxRate, string productType, decimal Area, decimal costPerSquareFoot,
            decimal laborCostPerSquareFoot, decimal materialCost, decimal laborCost, decimal tax, decimal total);

        Order EditOrder(Order order, DateTime orderDate, string newCustomerName, string newStateName, string newProductType, decimal newArea);

        Order RemoveOrder(Order order, DateTime orderDate, int orderNumber);

        Order LoadOrder(DateTime orderDate);

        void SaveOrder(Order order);
    }
}
