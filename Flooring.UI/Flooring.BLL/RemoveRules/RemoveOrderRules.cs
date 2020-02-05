using Flooring.Data;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL.RemoveRules
{
    public class RemoveOrderRules : IRemoveOrder
    {
        public RemoveOrderResponse RemoveOrder(Order order, DateTime orderDate, int orderNumber)
        {
            List<Order> Orders = ProductionRepository.GetOrders();

            List<Products> Products = ProductionRepository.GetProducts();

            List<Taxes> Taxes = ProductionRepository.GetTaxes();

            RemoveOrderResponse response = new RemoveOrderResponse();

            if (!Orders.Any(o => o.OrderDate == orderDate && o.OrderNumber == orderNumber))
            {
                response.Success = false;
                response.Message = "Error: That Order Cannot Be Found In Our System. Please Check Date and Order Number and Re-Enter.";
                return response;
            }
            else
            {
                response.Order = order;

                response.OrderDate = orderDate;

                response.OrderNumber = orderNumber;

                response.Success = true;

                response.Message = "Order Found";

                return response;
                
            }
        }
    }
}
