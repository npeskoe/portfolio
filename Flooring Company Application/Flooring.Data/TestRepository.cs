using Flooring.Models;
using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data
{
    public class TestRepository : IOrderRepository
    {
        private static Order _order = new Order
        {
            OrderDate = DateTime.Parse("09/11/1987"),
            OrderNumber = 1,
            CustomerName = "Wise",
            State = "OH",
            TaxRate = 6.25M,
            ProductType = "Wood",
            Area = 100,
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
        };

        private static List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            orders.Add(_order);

            return orders;
        }

        private static List<Products> GetProducts()
        {
            List<Products> _products = new List<Products>();

            _products.Add(new Products { ProductType = "Carpet", CostPerSquareFoot = 2.25M, LaborCostPerSquareFoot = 2.10M });
            _products.Add(new Products { ProductType = "Laminate", CostPerSquareFoot = 1.75M, LaborCostPerSquareFoot = 2.10M });
            _products.Add(new Products { ProductType = "Tile", CostPerSquareFoot = 3.50M, LaborCostPerSquareFoot = 4.15M });
            _products.Add(new Products { ProductType = "Wood", CostPerSquareFoot = 5.15M, LaborCostPerSquareFoot = 4.75M });

            return _products;
        }

        private static List<Taxes> GetTaxes()
        {
            List<Taxes> _taxes = new List<Taxes>();

            _taxes.Add(new Taxes { StateAbbreviation = "OH", StateName = "Ohio", TaxRate = 6.25M });
            _taxes.Add(new Taxes { StateAbbreviation = "PA", StateName = "Pennsylvania", TaxRate = 6.75M });
            _taxes.Add(new Taxes { StateAbbreviation = "MI", StateName = "Michigan", TaxRate = 5.75M });
            _taxes.Add(new Taxes { StateAbbreviation = "IN", StateName = "Indiana", TaxRate = 6.00M });

            return _taxes;
        }

        public Order AddOrder(Order order, DateTime orderDate, string customerName, string stateName, string productType, decimal area)
        {
            order.OrderDate = orderDate;

            order.CustomerName = customerName;

            order.State = stateName;

            order.ProductType = productType;

            order.Area = area;

            return order;
        }

        public Order CalculateOrder(Order order, string customerName, string stateName, string productType, decimal Area)
        {
            List<Order> orders = GetOrders();

            List<Products> products = GetProducts();

            List<Taxes> taxes = GetTaxes();

            order.OrderNumber = orders.Count() + 1;

            order.Area = Area;

            order.State = stateName;

            order.ProductType = productType;

            var loadedOrders = taxes.Where(t => t.StateAbbreviation == stateName);

            foreach (var o in loadedOrders)
            {
                order.TaxRate = o.TaxRate;
            }

            var loadedProducts = products.Where(p => p.ProductType == productType);

            foreach (var p in loadedProducts)
            {
                order.CostPerSquareFoot = p.CostPerSquareFoot;
                order.LaborCostPerSquareFoot = p.LaborCostPerSquareFoot;
            }

            return order;
        }

        public Order LoadOrder(DateTime OrderDate)
        {
            return _order;
        }

        public Order WriteOrder(Order order, DateTime orderDate, int orderNumber, string customerName, string stateName, decimal taxRate, string productType, decimal Area, decimal CostPerSquareFoot,
            decimal LaborCostPerSquareFoot, decimal MaterialCost, decimal LaborCost, decimal Tax, decimal Total)
        {
            return _order;
        }

        public Order RemoveOrder(Order order, DateTime orderDate, int orderNumber)
        {
            List<Order> orders = GetOrders();

            orders.Remove(order);

            return order;
        }

        public Order EditOrder(Order order, DateTime orderDate, string newCustomerName, string newStateName, string newProductType, decimal newArea)
        {
            order.OrderDate = orderDate;

            List<Order> Orders = GetOrders();

            if (newCustomerName == "")
            {
                var loadedOrders = Orders.Where(o => o.OrderDate == orderDate);

                foreach (var o in loadedOrders)
                {
                    newCustomerName = o.CustomerName;

                    o.CustomerName = newCustomerName;

                    o.State = newStateName;

                    o.ProductType = newProductType;

                    o.Area = newArea;

                    return order;
                }
            }
            else
            {
                var loadedOrders = Orders.Where(o => o.OrderDate == orderDate);

                foreach (var o in loadedOrders)
                {
                    o.CustomerName = newCustomerName;

                    o.State = newStateName;

                    o.ProductType = newProductType;

                    o.Area = newArea;

                    return order;
                }
            }
            return order;
        }

        public void SaveOrder(Order order)
        {
            _order = order;
        }
    }
}

