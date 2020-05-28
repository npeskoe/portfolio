using Flooring.Data;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL.AddRules
{
    public class AddOrderRules : IAddOrder
    {
        public AddOrderResponse AddOrder(Order order, DateTime orderDate, string customerName, string stateName, string productType, decimal Area)
        {

            List<Order> Orders = ProductionRepository.GetOrders();

            List<Products> Products = ProductionRepository.GetProducts();

            List<Taxes> Taxes = ProductionRepository.GetTaxes();

            AddOrderResponse response = new AddOrderResponse();

            if (orderDate <= DateTime.Today)
            {
                response.Success = false;
                response.Message = "Error: Order Date Must Be in the Future.";
                return response;
            }
            if (string.IsNullOrEmpty(customerName) == true)
            {
                response.Success = false;
                response.Message = "Error: Customer Name Cannot Be Blank.";
                return response;
            }
            if (!Taxes.Any(t => t.StateAbbreviation == stateName))
            {
                response.Success = false;
                response.Message = "Error: Incorrect State Code - Please Check Records and Re-Enter.";
                return response;
            }
            if (!Products.Any(p => p.ProductType.ToUpper() == productType.ToUpper()))
            {
                response.Success = false;
                response.Message = "Error: That is Not a Current Product in Our System.";
                return response;
            }
            
            if (Area <= 0)
            {
                response.Success = false;
                response.Message = "Error: Area Must be a Positive Number.";
                return response;
            }
            else
            {
                response.OrderDate = orderDate;

                response.CustomerName = customerName;

                response.StateName = stateName;

                response.ProductType = productType;

                response.Area = Area;

                response.Success = true;

                response.Message = "Order Added Successfully!";

                return response;
            }
        }
    }
}
