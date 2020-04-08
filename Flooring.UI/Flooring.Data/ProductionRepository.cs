using Flooring.Models;
using Flooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Data
{
    public class ProductionRepository : IOrderRepository
    {
        public static DirectoryInfo directory = new DirectoryInfo(@"C:\Data\FlooringMastery\Orders");

        public string filePath = (@"C:\Data\FlooringMastery\Orders");

        public static FileInfo[] files = directory.GetFiles("Orders_********");

        public static List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            foreach (var file in files)
            {
                string filePath;

                filePath = file.FullName;

                using (StreamReader sr = new StreamReader(filePath))
                {
                    sr.ReadLine();
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string format = "MMddyyyy";

                        Order order = new Order();

                        string iDate = file.Name.Substring(7,8);

                        order.OrderDate = DateTime.ParseExact(iDate, "MMddyyyy", CultureInfo.InvariantCulture);

                        string[] columns = line.Split(',');
                        order.OrderNumber = int.Parse(columns[0]);
                        order.CustomerName = columns[1];
                        order.State = columns[2];
                        order.TaxRate = decimal.Parse(columns[3]);
                        order.ProductType = columns[4];
                        order.Area = decimal.Parse(columns[5]);
                        order.CostPerSquareFoot = decimal.Parse(columns[6]);
                        order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);

                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        public static List<Products> GetProducts()
        {
            string filePath = @"C:\Data\FlooringMastery\Products.txt";

            List<Products> products = new List<Products>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();

                List<string> lines = new List<string>();

                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
                foreach (var p in lines)
                {
                    Products product = new Products();

                    string[] columns = p.Split(',');

                    product.ProductType = columns[0];

                    product.CostPerSquareFoot = decimal.Parse(columns[1]);

                    product.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                    products.Add(product);
                }
                return products;
            }
        }

        public static List<Taxes> GetTaxes()
        {
            string filePath = @"C:\Data\FlooringMastery\Taxes.txt";

            List<Taxes> taxes = new List<Taxes>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();

                List<string> lines = new List<string>();

                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
                foreach (var p in lines)
                {
                    Taxes tax = new Taxes();

                    string[] columns = p.Split(',');

                    tax.StateAbbreviation = columns[0];

                    tax.StateName = columns[1];

                    tax.TaxRate = decimal.Parse(columns[2]);

                    taxes.Add(tax);
                }
                return taxes;
            }
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

        public Order CalculateOrder(Order order, string customerName, string stateName, string productType, decimal area)
        {
            List<Order> orders = GetOrders();

            List<Products> products = GetProducts();

            List<Taxes> taxes = GetTaxes();

            order.OrderNumber = orders.Count() + 1;

            order.Area = area;

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

        public Order WriteOrder(Order order, DateTime orderDate, int orderNumber, string customerName, string stateName, decimal taxRate, string productType, decimal Area, decimal CostPerSquareFoot,
            decimal LaborCostPerSquareFoot, decimal MaterialCost, decimal LaborCost, decimal Tax, decimal Total)
        {
            

            var fileName = File.Create(directory + @"\Orders_" + orderDate.Date.ToString("MMddyyyy") + ".txt");

            using (StreamWriter sw = new StreamWriter(fileName))
            {
                sw.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
                sw.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", orderNumber, customerName, stateName, taxRate, productType, Math.Round(Area,2), CostPerSquareFoot,Math.Round(MaterialCost,2),Math.Round(LaborCost,2),Math.Round(Tax,2),Math.Round(Total,2));
            }
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

                    order.CustomerName = newCustomerName;

                    order.State = newStateName;

                    order.ProductType = newProductType;

                    order.Area = newArea;

                    return order;
                }
            }
            else
            {
                var loadedOrders = Orders.Where(o => o.OrderDate == orderDate);

                foreach (var o in loadedOrders)
                {
                    order.CustomerName = newCustomerName;

                    order.State = newStateName;

                    order.ProductType = newProductType;

                    order.Area = newArea;

                    return order;
                }
            }
            return order;
        }

        public Order LoadOrder(DateTime orderDate)
        {
            var getOrders = GetOrders();

            var loadedOrder = getOrders.Where(o => o.OrderDate == orderDate);

            foreach (var o in loadedOrder)
            {
                return o;
            }
            return null;
        }

        public void SaveOrder(Order order)
        {
            var loadOrders = GetOrders();

            var targetOrder = loadOrders.Where(o => o.OrderDate == order.OrderDate);

            Order _order;

            foreach (var o in targetOrder)
            {
                _order = order;
            }
        }

        public Order RemoveOrder(Order order, DateTime orderDate, int orderNumber)
        {
            { 
                File.Delete(directory + @"\Orders_" + orderDate.Date.ToString("MMddyyyy") + ".txt");
            }

            return order;
        }
    }
}
