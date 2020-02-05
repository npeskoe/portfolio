using Flooring.BLL;
using Flooring.BLL.AddRules;
using Flooring.BLL.CalculateRules;
using Flooring.BLL.EditRules;
using Flooring.BLL.RemoveRules;
using Flooring.Models;
using Flooring.Models.Interfaces;
using Flooring.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Tests
{
    [TestFixture]
    class OrderTests
    {
        [TestCase("09/11/2020", "Bob's Flooring", "IN", "Laminate", 60, true)]
        [TestCase("01/02/2020", "Acme Inc", "PA", "Tile", 100, true)]
        [TestCase("02/03/2020", "Bob's Carpet", "MI", "Carpet", 55, true)]
        [TestCase("01/01/1999", "Flooring Gallery", "IN", "Wood", 20, false)]

        public void CanAddOrderTest(DateTime orderDate, string customerName, string stateName, string productType,
            decimal Area, bool expectedResult)
        {
            IAddOrder addOrder;

            AddOrderRules addOrderRules = new AddOrderRules();

            addOrder = addOrderRules;

            Order order = new Order();

            orderDate = order.OrderDate;

            customerName = order.CustomerName;

            stateName = order.State;

            productType = order.ProductType;

            Area = order.Area;

            AddOrderResponse response = addOrder.AddOrder(order, orderDate, customerName, stateName, productType, Area);

            expectedResult = response.Success;

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("1","Big Al's Floors","PA","Carpet",60,6.75,2.25,2.10,135,126,17.62,true)]
        [TestCase("2","Bobby's Carpet Mart","OH","Tile",50,6.00,3.50,4.15,175,207.50,23.91,false)]

        public void CalculateOrderTest(int orderNumber, string customerName, string state, string productType, decimal Area, decimal taxRate, decimal costPerSquareFoot, decimal laborCostPerSquareFoot, decimal materialCost, decimal laborCost, decimal tax, bool expectedResult)
        {
            ICalculateOrder calculate;

            CalculateOrderRules calculateRule = new CalculateOrderRules();

            calculate = calculateRule;

            Order order = new Order();

            orderNumber = order.OrderNumber;
            customerName = order.CustomerName;
            state = order.State;
            productType = order.ProductType;
            Area = order.Area;
            taxRate = order.TaxRate;
            costPerSquareFoot = order.CostPerSquareFoot;
            laborCostPerSquareFoot = order.LaborCostPerSquareFoot;
            materialCost = order.MaterialCost;
            tax = order.Tax;

            CalculateOrderResponse response = calculate.CalculateOrder(customerName, productType, Area, state, taxRate, costPerSquareFoot, laborCostPerSquareFoot, materialCost, laborCost, tax);

            expectedResult = response.Success;

            Assert.AreEqual(expectedResult, response.Success);
            Assert.AreEqual(order.Tax, response.Tax);
            Assert.AreEqual(order.TaxRate, response.TaxRate);
            Assert.AreEqual(order.CostPerSquareFoot, response.CostPerSquareFoot);
            Assert.AreEqual(order.LaborCostPerSquareFoot, response.LaborCostPerSquareFoot);
            Assert.AreEqual(order.MaterialCost, response.MaterialCost);

        }

        [TestCase("01/01/2020", "Mike Thomas", "MI","Wood",100,true)]
        [TestCase("01/02/2020","","OH","Carpet",50,true)]
        [TestCase("09/11/1999","Tom's Flooring Outlet","VA","Laminate",25,false)]

        public void EditOrderTest(DateTime orderDate, string customerName, string stateName, string productType, decimal area,bool expectedResult)
        {
            IEditOrder edit;

            EditOrderRules editRule = new EditOrderRules();

            edit = editRule;

            Order order = new Order();

            orderDate = order.OrderDate;
            customerName = order.CustomerName;
            stateName = order.State;
            productType = order.ProductType;
            area = order.Area;

            EditOrderResponse response = edit.EditOrder(order, orderDate, customerName, stateName, productType, area);

            expectedResult = response.Success;

            Assert.AreEqual(expectedResult, response.Success);
            Assert.AreEqual(order.State, response.NewStateName);
        }

        [TestCase("01/01/2020","2",true)]
        [TestCase("02/02/2020","45",true)]

        public void RemoveOrderTest(DateTime orderDate, int orderNumber,bool expectedResult)
        {
            IRemoveOrder remove;

            RemoveOrderRules removeRule = new RemoveOrderRules();

            remove = removeRule;

            Order order = new Order();

            orderDate = order.OrderDate;
            orderNumber = order.OrderNumber;

            RemoveOrderResponse response = remove.RemoveOrder(order, orderDate, orderNumber);

            expectedResult = response.Success;

            Assert.AreEqual(expectedResult, response.Success);

        }
    }
}



