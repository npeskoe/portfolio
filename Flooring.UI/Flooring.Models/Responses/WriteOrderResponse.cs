using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models.Responses
{
    public class WriteOrderResponse : Response
    {
        public Order Order { get; set; }
        public int OrderNumber { get; set; }
        public int CustomerName { get; set; }
        public int State { get; set; }
        public decimal TaxRate { get; set; }
        public int ProductType { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSquareFoot { get; set; }
        public decimal LaborCostPerSquareFoot { get; set; }
        public decimal MaterialCost
        {
            get { return Area * CostPerSquareFoot; }
        }
        public decimal LaborCost
        {
            get { return Area * LaborCostPerSquareFoot; }
        }

        public decimal Tax
        {
            get { return (MaterialCost + LaborCost) * (TaxRate / 100); }
        }

        public decimal Total
        {
            get { return MaterialCost + LaborCost + Tax; }
        }
    }
}
