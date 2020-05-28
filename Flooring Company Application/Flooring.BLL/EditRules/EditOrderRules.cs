using Flooring.Data;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL.EditRules
{
    public class EditOrderRules : IEditOrder
    {
        public EditOrderResponse EditOrder(Order order, DateTime orderDate, string newCustomerName, string newStateName, string newProductType, decimal newArea)
        {
            List<Order> Orders = ProductionRepository.GetOrders();

            List<Products> Products = ProductionRepository.GetProducts();

            List<Taxes> Taxes = ProductionRepository.GetTaxes();

            EditOrderResponse response = new EditOrderResponse();

            if (!Orders.Any(o => o.OrderDate == orderDate))
            {
                response.Success = false;
                response.Message = "Error: That Order Cannot be Found in Our System. Please Check Date and Re-Enter.";
                return response;
            }
            if (!Taxes.Any(t => t.StateAbbreviation == newStateName))
            {
                response.Success = false;
                response.Message = "Error: Incorrect State Code - Please Check Records and Re-Enter.";
                return response;
            }
            if (!Products.Any(p => p.ProductType.ToUpper() == newProductType.ToUpper()))
            {
                response.Success = false;
                response.Message = "Error: That is Not a Current Product in Our System.";
                return response;
            }
            if (newArea <= 0)
            {
                response.Success = false;
                response.Message = "Error: Area Must be a Positive Number.";
                return response;
            }
            else
            {
                response.NewCustomerName = newCustomerName;
                response.NewStateName = newStateName;
                response.NewProductType = newProductType;
                response.NewArea = newArea;

                response.Success = true;

                response.Message = "Order Successfully Changed!";

                return response;
            }
        }
    }
}
