using Flooring.BLL;
using Flooring.Models;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.UI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            bool notValidOrder = true;

            while (notValidOrder)
            {
                Console.Clear();
                OrderManager orderManager = OrderManagerFactory.Create();

                Order order = new Order();

                bool notValidDate = true;

                while (notValidDate)
                {
                    DateTime orderDate;
                    Console.WriteLine("Please Enter the Order Date: (MMDDYYYY): ");
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

                bool notValidNameEntry = true;

                while (notValidNameEntry)
                {
                    Console.WriteLine("Please enter the Customer Name: ");
                    order.CustomerName = Console.ReadLine();

                    if (string.IsNullOrEmpty(order.CustomerName))
                    {
                        Console.WriteLine("Customer Name Cannot Be Empty. Please Enter Name Again.");
                        notValidNameEntry = true;
                    }
                    else
                    {
                        notValidNameEntry = false;
                        break;
                    }
                }
                Console.WriteLine();

                bool notValidStateEntry = true;

                while (notValidStateEntry)
                {
                    Console.WriteLine("Please Enter Two-Digit State Code: ");
                    order.State = Console.ReadLine().ToUpper();

                    if (string.IsNullOrEmpty(order.State))
                    {
                        Console.WriteLine("State Name Cannot Be Empty. Please Enter State Again.");
                        notValidStateEntry = true;
                    }
                    else if (order.State.All(char.IsDigit))
                    {
                        Console.WriteLine("State Name Cannot Contain Numbers. Please Enter State Again.");
                        notValidStateEntry = true;
                    }
                    else if (order.State.Count() > 2)
                    {
                        Console.WriteLine("State Name Must Be Only 2 Digits. Please Enter State Again.");
                        notValidStateEntry = true;
                    }
                    else
                    {
                        notValidStateEntry = false;
                        break;
                    }
                }
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
                    Console.WriteLine();
                    Console.WriteLine("Please Select Product Name: ");
                    Console.WriteLine();

                    foreach (var p in products)
                    {
                        Console.WriteLine(p.ProductType + " $" + p.CostPerSquareFoot);
                    }
                    Console.WriteLine();
                    string ptype = Console.ReadLine();

                    order.ProductType = ptype.Substring(0, 1).ToUpper() + ptype.Substring(1);

                    Console.WriteLine();

                    bool notValidArea = true;

                    while (notValidArea)
                    {
                        Console.WriteLine("Please Enter Flooring Area: ");

                        string userInput = Console.ReadLine();

                        if (decimal.TryParse(userInput, out decimal area))
                        {
                            order.Area = area;
                            notValidArea = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Flooring Area Must be a Number.");
                            notValidArea = true;
                        }
                    }

                    AddOrderResponse addOrder = orderManager.AddOrder(order, order.OrderDate, order.CustomerName, order.State, order.ProductType, order.Area);
                    if (addOrder.Success)
                    {
                        CalculateOrderResponse calculateOrder = orderManager.CalculateOrder(order, order.CustomerName, order.State, order.ProductType, order.Area);

                        if (calculateOrder.Success)
                        {
                            ConsoleIO.DisplayOrderDetails(order);
                            Console.WriteLine();

                            bool notYesOrNo = true;

                            while (notYesOrNo)
                            {
                                Console.WriteLine("Are You Sure You Want to Add This Order? (Y/N)");
                                string input = Console.ReadLine().ToUpper();

                                if (input == "Y")
                                {
                                    WriteOrderResponse writeOrder = orderManager.WriteOrder(order, order.OrderDate, order.OrderNumber, order.CustomerName, order.State,
                                    order.TaxRate, order.ProductType, order.Area, order.CostPerSquareFoot, order.LaborCostPerSquareFoot, order.MaterialCost, order.LaborCost, order.Tax, order.Total);

                                    if (writeOrder.Success)
                                    {
                                        Console.Clear();
                                        Console.WriteLine(writeOrder.Message);
                                        notValidOrder = false;
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("An error occured: ");
                                        Console.WriteLine(addOrder.Message);
                                        Console.WriteLine("Press any key to continue...");
                                        Console.ReadKey();
                                        break;
                                    }
                                }
                                if (input == "N")
                                {
                                    System.Environment.Exit(1);
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
                            Console.Clear();
                            Console.WriteLine("An error occured: ");
                            Console.WriteLine(addOrder.Message);
                            notValidOrder = true;
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                        }
                    }

                    else
                    {
                        Console.Clear();
                        Console.WriteLine("An error occured: ");
                        Console.WriteLine(addOrder.Message);
                        notValidOrder = true;
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    }
                }
            }
        }
    }

    







