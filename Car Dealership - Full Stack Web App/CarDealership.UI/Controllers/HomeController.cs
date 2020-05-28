using CarDealership.Data.Dapper;
using CarDealership.Data.Factory;
using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            List<FeaturedSpecialsViewModel> viewmodels = new List<FeaturedSpecialsViewModel>();

            var specials = SalesSpecialsRepositoryFactory.GetRepository().GetAllSpecials().ToList();
            var featured = VehicleRepositoryFactory.GetRepository().GetFeatured().ToList();

            foreach (var special in specials)
            {
                FeaturedSpecialsViewModel vm = new FeaturedSpecialsViewModel();

                vm.SpecialID = special.SpecialID;
                vm.SpecialName = special.SpecialName;
                vm.SpecialDescription = special.SpecialDescription;

                viewmodels.Add(vm);
            }

            foreach (var vehicle in featured)
            {
                FeaturedSpecialsViewModel vm = new FeaturedSpecialsViewModel();

                vm.VehicleID = vehicle.VehicleID;
                vm.ImageFileName = vehicle.ImageFileName;
                vm.YearBuilt = vehicle.YearBuilt;
                vm.MakeName = vehicle.MakeName;
                vm.ModelName = vehicle.ModelName;
                vm.SalesPrice = vehicle.SalesPrice;

                viewmodels.Add(vm);
            }

            return View(viewmodels);
        }

        public ActionResult Contact()
        {
            var model = new ContactUsRequest();

            return View(model);
        }

        [HttpPost]
        public ActionResult InsertContactUsRequest(ContactUsRequest request)
        {
            var repo = ContactUsRepositoryFactory.GetRepository();

            if (ModelState.IsValid)
            {
                repo.Insert(request);

                return View("Index");
            }

            return View("Contact", request);
        }

        public ActionResult Specials()
        {
            var model = SalesSpecialsRepositoryFactory.GetRepository().GetAllSpecials();
            return View(model);
        }
    }
}