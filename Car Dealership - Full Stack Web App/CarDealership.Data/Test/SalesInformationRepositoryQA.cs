using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    public class SalesInformationRepositoryQA : ISalesInformationRepository
    {
        public List<SalesInformation> GetSales()
        {
            List<SalesInformation> sales = new List<SalesInformation>();

            sales.Add(new SalesInformation
            {
                SalesID = 1,
                CustomerName = "Monica Timms",
                CustomerPhone = "502-555-0156",
                CustomerEmail = "mtibbs@gmail.com",
                CustomerStreet1 = "123 E Main St",
                CustomerStreet2 = null,
                CustomerCity = "Louisville",
                CustomerState = "KY",
                CustomerZip = "40204",
                PurchasePrice = 15500,
                PurchaseTypeID = 1,
                VehicleID = 1,
                Id = "0c79a30f-8399-4b6f-9363-1f7c604a5d53",
                PurchaseDate = DateTime.Parse("05/04/2020")
            });

            return sales;
        }

        public void Insert(SalesInformation sale)
        {
            SalesInformation newSale = new SalesInformation();

            var repo = GetSales();

            newSale.CustomerName = sale.CustomerName;
            newSale.CustomerPhone = sale.CustomerPhone;
            newSale.CustomerEmail = sale.CustomerEmail;
            newSale.CustomerStreet1 = sale.CustomerStreet1;
            newSale.CustomerStreet2 = sale.CustomerStreet2;
            newSale.CustomerCity = sale.CustomerCity;
            newSale.CustomerState = sale.CustomerState;
            newSale.CustomerZip = sale.CustomerZip;
            newSale.PurchasePrice = sale.PurchasePrice;
            newSale.PurchaseTypeID = sale.PurchaseTypeID;
            newSale.VehicleID = sale.VehicleID;
            newSale.Id = sale.Id;
            newSale.PurchaseDate = sale.PurchaseDate;

            repo.Insert(newSale.SalesID, newSale);

        }
    }
}
