using CarDealership.Data.Factory;
using CarDealership.Models.Queries;
using CarDealership.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Details(int id)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var salesRepo = SalesInformationRepositoryFactory.GetRepository();
            var sales = salesRepo.GetSales();
            var model = repo.GetVehicleDetails(id);
            var available = repo.GetVehicleItems();

            model.IsSold = sales.Any(m => m.VehicleID == id);

            return View(model);
        }

        public ActionResult New()
        {
            return View();
        }

        public ActionResult Used()
        {
            return View();
        }
    }
}