using Flooring.BLL;
using Flooring.Models.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.UI.Workflows
{
    public class DisplayOrderWorkflow
    {
        public void Execute()
        {
            bool notValidOrder = true;

            while (notValidOrder)
            {
                OrderManager manager = OrderManagerFactory.Create();

                bool notValidDate = true;

                while (notValidDate)
                {
                    Console.Clear();
                    Console.WriteLine("Please Enter The Order Date (MMDDYYYY): ");

                    string userDate = Console.ReadLine();
                    string format = "MMddyyyy";

                    DateTime orderDate;

                    if (DateTime.TryParseExact(userDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out orderDate))
                    {
                        notValidDate = false;
                        OrderLookupResponse response = manager.LookupOrder(orderDate);

                        if (response.Success)
                        {
                            ConsoleIO.DisplayOrderDetails(response.Order);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            notValidOrder = false;
                        }
                        else
                        {
                            Console.WriteLine("An error occured: ");
                            Console.WriteLine(response.Message);
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            notValidDate = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("That is Not a Valid Date. Please Re-Enter Correct Order Date.");
                        notValidDate = true;
                    }
                }

            }
        }
    }
}
