using CarDealership.Data.Factory;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CarDealership.UI.Controllers
{
    public class SalesController : Controller
    {
        [Authorize(Roles ="sales")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Purchase(int id)
        {
            PurchaseSalesInfoViewModel vm = new PurchaseSalesInfoViewModel();

            var repo = VehicleRepositoryFactory.GetRepository();

            var car = repo.GetVehicleDetails(id);

            vm.VehicleID = car.VehicleID;
            vm.SalesTypeID = car.SalesTypeID;
            vm.YearBuilt = car.YearBuilt;
            vm.MakeName = car.MakeName;
            vm.ModelName = car.ModelName;
            vm.ImageFileName = car.ImageFileName;
            vm.BodyTypeName = car.BodyTypeName;
            vm.TransmissionTypeName = car.TransmissionTypeName;
            vm.ExtColorName = car.ExtColorName;
            vm.IntColorName = car.IntColorName;
            vm.Mileage = car.Mileage;
            vm.VINNumber = car.VINNumber;
            vm.SalesPrice = car.SalesPrice;
            vm.MSRP = car.MSRP;
            vm.VehicleDescription = car.VehicleDescription;

            return View(vm);           
            
           }

        [HttpPost]
        public ActionResult InsertSalesInformation(PurchaseSalesInfoViewModel model)
        {
            var inventory = VehicleRepositoryFactory.GetRepository();

            var vech = inventory.GetVehicleDetails(model.VehicleID);

            if (model.PurchasePrice >= vech.MSRP)
            {
                ModelState.AddModelError("PurchasePrice", "Purchase price cannot be greater than MSRP");
            }
            if (decimal.ToDouble(model.PurchasePrice) < ((decimal.ToDouble(vech.SalesPrice) * 0.95)))
            {
                ModelState.AddModelError("PurchasePrice", "Purchase price cannot be less than 95% of sales price");
            }

            if (ModelState.IsValid)
            {
                var repo = SalesInformationRepositoryFactory.GetRepository();

                SalesInformation sale = new SalesInformation();

                sale.VehicleID = model.VehicleID;

                sale.CustomerName = model.CustomerName;

                sale.CustomerPhone = model.CustomerPhone;

                sale.CustomerEmail = model.CustomerEmail;

                sale.CustomerStreet1 = model.CustomerStreet1;

                sale.CustomerStreet2 = model.CustomerStreet2;

                sale.CustomerCity = model.CustomerCity;

                sale.CustomerState = model.CustomerState;

                sale.CustomerZip = model.CustomerZip;

                sale.PurchasePrice = model.PurchasePrice;

                sale.PurchaseTypeID = model.PurchaseTypeID;

                var id = User.Identity.GetUserId();

                sale.Id = id;

                DateTime today = DateTime.Today;

                sale.PurchaseDate = today;

                repo.Insert(sale);

                return RedirectToAction("Index");

            }
            else
            {
                var repo = VehicleRepositoryFactory.GetRepository();

                var car = repo.GetVehicleDetails(model.VehicleID);

                model.YearBuilt = car.YearBuilt;
                model.MakeName = car.MakeName;
                model.ModelName = car.ModelName;
                model.ImageFileName = car.ImageFileName;
                model.BodyTypeName = car.BodyTypeName;
                model.TransmissionTypeName = car.TransmissionTypeName;
                model.ExtColorName = car.ExtColorName;
                model.IntColorName = car.IntColorName;
                model.Mileage = car.Mileage;
                model.VINNumber = car.VINNumber;
                model.SalesPrice = car.SalesPrice;
                model.MSRP = car.MSRP;
                model.VehicleDescription = car.VehicleDescription;

                return View("Purchase", model);
            }

            
        }
    }
}