using Flooring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.UI
{
    public class ConsoleIO
    {
        public static void DisplayOrderDetails(Order order)
        {
            Console.Clear();
            Console.WriteLine("*******************************************");
            Console.WriteLine(order.OrderNumber + " ");
            Console.WriteLine(order.CustomerName);
            Console.WriteLine(order.State);
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Materials: " + "$" + Math.Round(order.MaterialCost,2));
            Console.WriteLine($"Labor: " + "$" + Math.Round(order.LaborCost,2));
            Console.WriteLine($"Tax: " + "$" + Math.Round(order.Tax,2));
            Console.WriteLine($"Total: " + "$" + Math.Round(order.Total,2));
            Console.WriteLine("*******************************************");
        }

        public static void ChangeOrderDetails(Order order)
        {
            Console.Clear();
            Console.WriteLine("New Order Details: ");
            Console.WriteLine();
            Console.WriteLine("*******************************************");
            Console.WriteLine(order.CustomerName);
            Console.WriteLine(order.State);
            Console.WriteLine($"Product: {order.ProductType}");
            Console.WriteLine($"Area: {order.Area}");
            Console.WriteLine("*******************************************");
        }
    }
}
