using Flooring.BLL;
using Flooring.Models;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.UI.Workflows
{
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            bool notValidOrder = true;

            while (notValidOrder)
            {
                OrderManager orderManager = OrderManagerFactory.Create();

                Order order = new Order();

                Console.Clear();

                bool notValidDate = true;

                while (notValidDate)
                {
                    DateTime orderDate;
                    Console.WriteLine("Please Enter the Order Date You Would Like to Remove: (MMDDYYYY): ");
                    string userDate = Console.ReadLine();
                    string format = "MMddyyyy";

                    if (DateTime.TryParseExact(userDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out orderDate))
                    {
                        order.OrderDate = orderDate;
                        notValidDate = false;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("That is Not a Valid Date. Please Re-Enter Correct Order Date.");
                        notValidDate = true;
                    }
                }
                Console.WriteLine();

                bool notValidNumber = true;

                while (notValidNumber)
                {
                    Console.WriteLine("Please Enter the Order Number You Want to Remove: ");
                    string oNum = Console.ReadLine();

                    if (int.TryParse(oNum, out int orderNumber))
                    {
                        order.OrderNumber = orderNumber;
                        notValidNumber = false;
                    }
                    else
                    {
                        Console.WriteLine("That is Not a Valid Order Number. Please Re-Enter.");
                        notValidNumber = true;
                    }
                }
                OrderLookupResponse lookupOrder = orderManager.LookupOrder(order.OrderDate);

                if (lookupOrder.Success)
                {
                    ConsoleIO.DisplayOrderDetails(lookupOrder.Order);

                    bool notYesOrNo = true;

                    while (notYesOrNo)
                    {
                        Console.WriteLine("Are You Sure You Want to Remove This Order (Y/N)?");
                        string input = Console.ReadLine().ToUpper();

                        if (input == "Y")
                        {
                            RemoveOrderResponse removeOrder = orderManager.RemoveOrder(order, order.OrderDate, order.OrderNumber);

                            if (removeOrder.Success)
                            {
                                Console.Clear();
                                Console.WriteLine("Order Removed!");
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                notValidOrder = false;
                                break;
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("An error occured: ");
                                Console.WriteLine(removeOrder.Message);
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                                notValidOrder = true;
                            }
                        }
                        if (input == "N")
                        {
                            Environment.Exit(1);
                        }
                        else
                        {
                            Console.WriteLine("Please Enter Y or N.");
                            notYesOrNo = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("An error occured: ");
                    Console.WriteLine(lookupOrder.Message);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    notValidOrder = true;
                }
            }
        }
    }
}

