using CarDealership.Data.Dapper;
using CarDealership.Data.Factory;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Tests.IntegrationTests
{
    [TestFixture]
    public class DapperTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["GuildCars"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadBodyTypes()
        {
            var repo = new BodyTypesRepositoryDapper();

            var bodyTypes = repo.GetAll();

            Assert.AreEqual(4, bodyTypes.Count);

            Assert.AreEqual(1, bodyTypes[0].BodyTypeID);
            Assert.AreEqual("Car", bodyTypes[0].BodyTypeName);
        }

        [Test]
        public void CanLoadCarMakeItems()
        {
            var repo = new CarMakesRepositoryDapper();

            var carMakeItems = repo.GetAll();

            Assert.AreEqual(3, carMakeItems.Count);

            Assert.AreEqual(1, carMakeItems[0].MakeID);
            Assert.AreEqual("Acura", carMakeItems[0].MakeName);
            Assert.AreEqual("test@test.com", carMakeItems[0].Email);
        }

        [Test]
        public void CanLoadCarModelItems()
        {
            var repo = new CarModelItemRepositoryDapper();

            var carModelItems = repo.GetAll();

            Assert.AreEqual(4, carModelItems.Count);

            Assert.AreEqual(1, carModelItems[0].ModelID);
            Assert.AreEqual("MDX", carModelItems[0].ModelName);
            Assert.AreEqual("test@test.com", carModelItems[0].Email);
        }

        [Test]
        public void CanLoadSalesVehicleTypes()
        {
            var repo = new SalesTypesRepositoryDapper();

            var salesTypes = repo.GetAll();

            Assert.AreEqual(2, salesTypes.Count);
            Assert.AreEqual(1, salesTypes[0].SalesTypeID);
            Assert.AreEqual("New", salesTypes[0].SalesTypeName);
        }

        [Test]
        public void CanLoadTransmissionTypes()
        {
            var repo = new TransmissionTypesRepositoryDapper();

            var transmissionTypes = repo.GetAll();

            Assert.AreEqual(2, transmissionTypes.Count);
            Assert.AreEqual(1, transmissionTypes[0].TransmissionID);
            Assert.AreEqual("Automatic", transmissionTypes[0].TransmissionTypeName);
        }

        [Test]
        public void CanLoadExtColors()
        {
            var repo = new ExtColorsRepositoryDapper();

            var extColors = repo.GetAll();

            Assert.AreEqual(6, extColors.Count);
            Assert.AreEqual(1, extColors[0].ExtColorID);
            Assert.AreEqual("White", extColors[0].ExtColorName);
        }

        [Test]
        public void CanLoadIntColors()
        {
            var repo = new IntColorsRepositoryDapper();

            var intColors = repo.GetAll();

            Assert.AreEqual(5, intColors.Count);
            Assert.AreEqual(1, intColors[0].IntColorID);
            Assert.AreEqual("Black", intColors[0].IntColorName);
        }

        [Test]
        public void CanLoadVehicle()
        {
            var repo = new VehicleInventoryRepositoryDapper();

            var vehicle = repo.GetByID(1);

            Assert.IsNotNull(vehicle);

            Assert.AreEqual(1, vehicle.VehicleID);
            Assert.AreEqual(1, vehicle.ModelID);
            Assert.AreEqual(1, vehicle.MakeID);
            Assert.AreEqual(1, vehicle.SalesTypeID);
            Assert.AreEqual(2, vehicle.BodyTypeID);
            Assert.AreEqual(2020, vehicle.YearBuilt);
            Assert.AreEqual(1, vehicle.TransmissionID);
            Assert.AreEqual(1, vehicle.ExtColorID);
            Assert.AreEqual(3, vehicle.IntColorID);
            Assert.AreEqual(46882, vehicle.Mileage);
            Assert.AreEqual("5J8TB4H3XHL018174", vehicle.VINNumber);
            Assert.AreEqual(25620, vehicle.MSRP);
            Assert.AreEqual(22350, vehicle.SalesPrice);
            Assert.AreEqual("Nice and Clean Acura MDX", vehicle.VehicleDescription);
            Assert.AreEqual(false, vehicle.IsFeaturedVehicle);
            Assert.AreEqual("placeholder.png", vehicle.ImageFileName);

        }

        [Test]
        public void NotFoundVehicleReturnsNull()
        {
            var repo = new VehicleInventoryRepositoryDapper();

            var vehicle = repo.GetByID(100000);

            Assert.IsNull(vehicle);
        }

        [Test]
        public void CanAddVehicle()
        {
            VehicleInventory vehicleToAdd = new VehicleInventory();

            var repo = new VehicleInventoryRepositoryDapper();

            vehicleToAdd.ModelID = 2;
            vehicleToAdd.MakeID = 1;
            vehicleToAdd.SalesTypeID = 1;
            vehicleToAdd.BodyTypeID = 3;
            vehicleToAdd.YearBuilt = 2020;
            vehicleToAdd.TransmissionID = 1;
            vehicleToAdd.ExtColorID = 1;
            vehicleToAdd.IntColorID = 3;
            vehicleToAdd.Mileage = 233;
            vehicleToAdd.VINNumber = "3A4FY48B87T593601";
            vehicleToAdd.MSRP = 24900;
            vehicleToAdd.SalesPrice = 23000;
            vehicleToAdd.VehicleDescription = "Brand New!";
            vehicleToAdd.IsFeaturedVehicle = true;
            vehicleToAdd.ImageFileName = "placeholder.png";

            repo.AddVehicle(vehicleToAdd);

            Assert.AreEqual(5, vehicleToAdd.VehicleID);
        }

        [Test]
        public void CanUpdateListing()
        {
            VehicleInventory vehicleToAdd = new VehicleInventory();

            var repo = new VehicleInventoryRepositoryDapper();

            vehicleToAdd.ModelID = 1;
            vehicleToAdd.MakeID = 1;
            vehicleToAdd.SalesTypeID = 1;
            vehicleToAdd.BodyTypeID = 3;
            vehicleToAdd.YearBuilt = 2020;
            vehicleToAdd.TransmissionID = 1;
            vehicleToAdd.ExtColorID = 1;
            vehicleToAdd.IntColorID = 3;
            vehicleToAdd.Mileage = 233;
            vehicleToAdd.VINNumber = "3A4FY48B87T593601";
            vehicleToAdd.MSRP = 24900;
            vehicleToAdd.SalesPrice = 23000;
            vehicleToAdd.VehicleDescription = "Brand New!";
            vehicleToAdd.IsFeaturedVehicle = true;
            vehicleToAdd.ImageFileName = "placeholder.png";

            repo.AddVehicle(vehicleToAdd);

            vehicleToAdd.ModelID = 2;
            vehicleToAdd.MakeID = 2;
            vehicleToAdd.SalesTypeID = 2;
            vehicleToAdd.BodyTypeID = 3;
            vehicleToAdd.YearBuilt = 2017;
            vehicleToAdd.TransmissionID = 1;
            vehicleToAdd.ExtColorID = 2;
            vehicleToAdd.IntColorID = 1;
            vehicleToAdd.Mileage = 2300;
            vehicleToAdd.VINNumber = "3A4FY48Z87T593601";
            vehicleToAdd.MSRP = 22900;
            vehicleToAdd.SalesPrice = 22000;
            vehicleToAdd.VehicleDescription = "Slightly New!";
            vehicleToAdd.IsFeaturedVehicle = true;
            vehicleToAdd.ImageFileName = "updated.png";

            repo.UpdateVehicle(vehicleToAdd);

            var updatedVehicle = repo.GetByID(5);

            Assert.AreEqual(2, updatedVehicle.ModelID);
            Assert.AreEqual(2, updatedVehicle.MakeID);
            Assert.AreEqual(2, updatedVehicle.SalesTypeID);
            Assert.AreEqual(3, updatedVehicle.BodyTypeID);
            Assert.AreEqual(2017, updatedVehicle.YearBuilt);
            Assert.AreEqual(1, updatedVehicle.TransmissionID);
            Assert.AreEqual(2, updatedVehicle.ExtColorID);
            Assert.AreEqual(1, updatedVehicle.IntColorID);
            Assert.AreEqual(2300, updatedVehicle.Mileage);
            Assert.AreEqual("3A4FY48Z87T593601", updatedVehicle.VINNumber);
            Assert.AreEqual(22900, updatedVehicle.MSRP);
            Assert.AreEqual(22000, updatedVehicle.SalesPrice);
            Assert.AreEqual("Slightly New!", updatedVehicle.VehicleDescription);
            Assert.AreEqual(true, updatedVehicle.IsFeaturedVehicle);
            Assert.AreEqual("updated.png", updatedVehicle.ImageFileName);
        }

        [Test]
        public void CanDeleteVehicle()
        {
            VehicleInventory vehicleToAdd = new VehicleInventory();

            var repo = new VehicleInventoryRepositoryDapper();

            vehicleToAdd.ModelID = 1;
            vehicleToAdd.MakeID = 1;
            vehicleToAdd.SalesTypeID = 1;
            vehicleToAdd.BodyTypeID = 3;
            vehicleToAdd.YearBuilt = 2020;
            vehicleToAdd.TransmissionID = 1;
            vehicleToAdd.ExtColorID = 1;
            vehicleToAdd.IntColorID = 3;
            vehicleToAdd.Mileage = 233;
            vehicleToAdd.VINNumber = "3A4FY48B87T593601";
            vehicleToAdd.MSRP = 24900;
            vehicleToAdd.SalesPrice = 23000;
            vehicleToAdd.VehicleDescription = "Brand New!";
            vehicleToAdd.IsFeaturedVehicle = true;
            vehicleToAdd.ImageFileName = "placeholder.png";

            repo.AddVehicle(vehicleToAdd);

            var loaded = repo.GetByID(5);
            Assert.IsNotNull(loaded);

            repo.DeleteVehicle(5);

            loaded = repo.GetByID(5);

            Assert.IsNull(loaded);
        }
        [Test]
        public void CanLoadFeaturedVehicles()
        {
            var repo = new VehicleInventoryRepositoryDapper();

            List<FeaturedVehicles> featured = repo.GetFeatured().ToList();

            Assert.AreEqual(3, featured.Count());

            Assert.AreEqual(4, featured[0].VehicleID);
            Assert.AreEqual("Audi", featured[0].MakeName);
            Assert.AreEqual("A3", featured[0].ModelName);
            Assert.AreEqual(20000, featured[0].SalesPrice);
            Assert.AreEqual("placeholder.png", featured[0].ImageFileName);
            
        }
        [Test]
        public void CanLoadVehicleDetails()
        {
            var repo = new VehicleInventoryRepositoryDapper();

            var vehicle = repo.GetVehicleDetails(1);

            Assert.IsNotNull(vehicle);

            Assert.AreEqual(1, vehicle.VehicleID);
            Assert.AreEqual(2020, vehicle.YearBuilt);
            Assert.AreEqual("Acura", vehicle.MakeName);
            Assert.AreEqual("MDX", vehicle.ModelName);
            Assert.AreEqual(46882, vehicle.Mileage);
            Assert.AreEqual("5J8TB4H3XHL018174", vehicle.VINNumber);
            Assert.AreEqual(25620, vehicle.MSRP);
            Assert.AreEqual(22350, vehicle.SalesPrice);
            Assert.AreEqual("Nice and Clean Acura MDX", vehicle.VehicleDescription);
            Assert.AreEqual("placeholder.png", vehicle.ImageFileName);
        }

        [Test]
        public void GetSalesSpecials()
        {
            var repo = new SalesSpecialsRepositoryDapper();

            var special = repo.GetAllSpecials();

            Assert.IsNotNull(special);

            Assert.AreEqual(1, special[0].SpecialID);
            Assert.AreEqual("Acura MDX Special", special[0].SpecialName);
            Assert.AreEqual("2.9% AND $750 Customer Rebate on a New MDX!", special[0].SpecialDescription);
        }

        [Test]
        public void CanInsertSalesSpecial()
        {
            SalesSpecials specialToAdd = new SalesSpecials();

            var repo = new SalesSpecialsRepositoryDapper();

            var specials = repo.GetAllSpecials();

            specialToAdd.SpecialName = "$10,000 Off MSRP";
            specialToAdd.SpecialDescription = "Save $10,000 on remaining 2019 Models in Stock.";

            repo.InsertSpecial(specialToAdd);

            Assert.AreEqual(3, specialToAdd.SpecialID);
            
        }

        [Test]
        public void CanAddCarMake()
        {
            CarMakes make = new CarMakes();

            var repo = new CarMakesRepositoryDapper();

            make.MakeName = "Buick";
            var dateTime = DateTime.Parse("04/20/2020");
            make.DateAdded = dateTime;
            make.Email = "npeskoe@gmail.com";

            repo.Insert(make);

            Assert.AreEqual(4, make.MakeID);
        }

        [Test]
        public void CanAddCarModel()
        {
            CarModels model = new CarModels();

            var repo = new CarModelRepositoryDaper();

            model.ModelName = "A4";
            var dateTime = DateTime.Parse("04/20/2020");
            model.DateAdded = dateTime;
            model.Email = "npeskoe@gmail.com";
            model.MakeID = 2;

            repo.Insert(model);

            Assert.AreEqual(5, model.ModelID);
        }

        [Test]
        public void CanViewDetailsForSearchResults()
        {
            var repo = new VehicleInventoryRepositoryDapper();

            List<VehicleItem> vehicleItems = repo.GetVehicleItems().ToList();

            Assert.AreEqual(2, vehicleItems.Count);
            Assert.AreEqual(1, vehicleItems[0].VehicleID);
            Assert.AreEqual(2020, vehicleItems[0].YearBuilt);
            Assert.AreEqual("Acura", vehicleItems[0].MakeName);
            Assert.AreEqual("MDX", vehicleItems[0].ModelName);
            Assert.AreEqual("placeholder.png", vehicleItems[0].ImageFileName);
            Assert.AreEqual("SUV", vehicleItems[0].BodyTypeName);
            Assert.AreEqual("White", vehicleItems[0].ExtColorName);
            Assert.AreEqual("Gray", vehicleItems[0].IntColorName);
            Assert.AreEqual(46882, vehicleItems[0].Mileage);
            Assert.AreEqual("5J8TB4H3XHL018174", vehicleItems[0].VINNumber);
            Assert.AreEqual(22350, vehicleItems[0].SalesPrice);
            Assert.AreEqual(25620, vehicleItems[0].MSRP);
            
        }

        [Test]
        public void CanInsertContactUsRequest()
        {
            ContactUsRequest request = new ContactUsRequest();

            var repo = new ContactUsRepositoryDapper();

            request.ContactName = "Jim Duggin";
            request.ContactEmail = "jim@hacksaw.com";
            request.ContactPhone = "111-222-3333";
            request.ContactMessage = "Please call me about the new BMW";
            
            repo.Insert(request);

            Assert.AreEqual(2, request.ContactUsID);

        }

        [Test]
        public void CanInsertSalesInformation()
        {
            SalesInformation sale = new SalesInformation();

            var repo = new SalesInformationRepositoryDapper();

            sale.CustomerName = "Susan Wright";
            sale.CustomerPhone = "502-555-0117";
            sale.CustomerEmail = "thewrightstuff@yahoo.com";
            sale.CustomerStreet1 = "200 Merriweather Rd";
            sale.CustomerStreet2 = "Apt B";
            sale.CustomerCity = "Louisville";
            sale.CustomerState = "KY";
            sale.CustomerZip = "40223";
            sale.PurchasePrice = 18200;
            sale.PurchaseTypeID = 2;
            sale.VehicleID = 4;

            var dateTime = DateTime.Parse("04/20/2020");
            sale.PurchaseDate = dateTime;

            sale.Id = "00000000-0000-0000-0000-000000000000";

            repo.Insert(sale);

            Assert.AreEqual(3, sale.SalesID);
        }

        [Test]
        public void CanLoadSalesReport()
        {
            var repo = new SalesInformationRepositoryDapper();

            var sales = repo.GetSales();

            Assert.IsNotNull(sales);
        }

        [Test]
        public void CanLoadNewVehicleReport()
        {
            var repo = new VehicleInventoryReportRepositoryDapper();

            var newVehicleReport = repo.GetNewInventoryReport();

            Assert.IsNotNull(newVehicleReport);
        }

        [Test]
        public void CanLoadUsedVehicleReport()
        {
            var repo = new VehicleInventoryReportRepositoryDapper();

            var usedVehicleReport = repo.GetUsedInventoryReport();

            Assert.IsNotNull(usedVehicleReport);
        }

        [Test]
        public void CanLoadNewVehicles()
        {
            var repo = new VehicleInventoryRepositoryDapper();

            var newVehicles = repo.GetNewInventory();

            Assert.IsNotNull(newVehicles);
        }
        
        [Test]
        public void CanGetModelByMakes()
        {
            var repo = new CarModelItemRepositoryDapper();

            var carModels = repo.GetModelsByMake("Audi");

            Assert.IsNotNull(carModels);
            Assert.AreEqual(carModels[0].ModelName, "A3");
        }
    }
}
