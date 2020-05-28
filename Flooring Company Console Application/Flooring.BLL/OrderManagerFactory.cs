using Flooring.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.BLL
{
    public class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "TestRepository":
                    return new OrderManager(new TestRepository());
                case "ProductionRepository":
                    return new OrderManager(new ProductionRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}
