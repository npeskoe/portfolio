using Flooring.BLL.AddRules;
using Flooring.BLL.CalculateRules;
using Flooring.BLL.EditRules;
using Flooring.BLL.RemoveRules;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderLookupResponse LookupOrder(DateTime orderDate)
        {
            OrderLookupResponse response = new OrderLookupResponse();

            response.Order = _orderRepository.LoadOrder(orderDate);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "That order is not a valid order in our system.";
                return response;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public AddOrderResponse AddOrder(Order order, DateTime orderDate, string customerName, string stateName, string productType, decimal area)
        {
            AddOrderResponse response = new AddOrderResponse();

            response.Order = _orderRepository.AddOrder(order, orderDate, customerName, stateName, productType, area);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "That order is not a valid order in our system. Please Check Order Information and Re-Enter.";
                return response;
            }
            else
            {
                response.Success = true;
            }

            IAddOrder addOrderRule = AddOrderFactory.Create();
            response = addOrderRule.AddOrder(order, orderDate, customerName, stateName, productType, area);

            return response;
        }

        public CalculateOrderResponse CalculateOrder(Order order, string customerName, string state, string productType, decimal Area)
        {
            CalculateOrderResponse response = new CalculateOrderResponse();

            response.Order = _orderRepository.CalculateOrder(order, customerName, state, productType, Area);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "Error: Unable to add order. See IT.";
                return response;
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public WriteOrderResponse WriteOrder(Order order, DateTime orderDate, int orderNumber, string customerName, string stateName, decimal taxRate, string productType, decimal Area, decimal CostPerSquareFoot,
            decimal LaborCostPerSquareFoot, decimal MaterialCost, decimal LaborCost, decimal Tax, decimal Total)
        {
            WriteOrderResponse response = new WriteOrderResponse();

            response.Order = _orderRepository.WriteOrder(order, orderDate, orderNumber, customerName, stateName, taxRate, productType, Area, CostPerSquareFoot, LaborCostPerSquareFoot, MaterialCost, LaborCost, Tax, Total);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "Error: Unable to add order. See IT.";
                return response;
            }
            else
            {
                response.Success = true;
                response.Message = "Order Added Successfully!";
            }
            return response;
        }

        public EditOrderResponse EditOrder(Order order, DateTime orderDate, string newCustomerName, string newStateName, string newProductType, decimal newArea)
        {
            EditOrderResponse response = new EditOrderResponse();

            response.Order = _orderRepository.EditOrder(order, orderDate, newCustomerName, newStateName, newProductType, newArea);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "That order is not a valid order in our system. Please Check Order Information and Re-Enter.";
                return response;
            }
            else
            {
                response.Success = true;
            }
            IEditOrder editOrderRule = EditOrderFactory.Create();
            response = editOrderRule.EditOrder(order, orderDate, order.CustomerName, order.State, order.ProductType, order.Area);

            return response;

        }
        public RemoveOrderResponse RemoveOrder(Order order, DateTime orderDate, int orderNumber)
        {
            RemoveOrderResponse response = new RemoveOrderResponse();

            response.Order = _orderRepository.RemoveOrder(order, orderDate, orderNumber);

            if (response.Order == null)
            {
                response.Success = false;
                response.Message = "That order is not a valid order in our system. Please Check Order Information and Re-Enter.";
                return response;
            }
            else
            {
                response.Success = true;
            }

            return response;
        }
    }
}


