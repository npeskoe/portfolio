using CarDealership.Data.Factory;
using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles ="admin")]
        public ActionResult Vehicles()
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            var model = repo.GetVehicleItems();

            return View(model);
        }

        [HttpGet]
        public ActionResult AddVehicle()
        {

            List<string> MakeNames = new List<string>();

            AddVehicleViewModel vm = new AddVehicleViewModel();

            var makeRepo = CarMakesRepositoryFactory.GetRepository();

            List<CarMakes> carMakes = makeRepo.GetAll();

            var carMakeNames = (from carmake in carMakes
                                select carmake.MakeName).ToList();

            vm.CarMakeNames = new SelectList(carMakes, "MakeName", "MakeName", 1);

            return View(vm);
        }

        [HttpPost]
        public ActionResult AddVehicle(AddVehicleViewModel model)
        {
            VehicleInventory vehicle = new VehicleInventory();

            var repo = VehicleRepositoryFactory.GetRepository();

            var makeRepo = CarMakesRepositoryFactory.GetRepository();

            List<CarMakes> carMakes = makeRepo.GetAll();

            var makeID = (from make in carMakes
                          where model.CarMakeName == make.MakeName
                          select make.MakeID).FirstOrDefault();

            vehicle.MakeID = makeID;

            var modelRepo = CarModelItemRepositoryFactory.GetRepository();

            List<CarModelItem> carModels = modelRepo.GetAll();

            var modelID = (from carmodel in carModels
                           where model.ModelName == carmodel.ModelName
                           select carmodel.ModelID).FirstOrDefault();

            vehicle.ModelID = modelID;

            vehicle.SalesTypeID = model.SalesTypeID;

            vehicle.BodyTypeID = model.BodyTypeID;

            vehicle.YearBuilt = model.YearBuilt;

            vehicle.TransmissionID = model.TransmissionID;

            vehicle.ExtColorID = model.ExtColorID;

            vehicle.IntColorID = model.IntColorID;

            vehicle.Mileage = model.Mileage;

            if (vehicle.SalesTypeID == 1 && vehicle.Mileage > 1000)
            {
                ModelState.AddModelError("Mileage", "Mileage on new car must be below 1000");
            }
            if (vehicle.SalesTypeID == 2 && vehicle.Mileage < 1000)
            {
                ModelState.AddModelError("Mileage", "Mileage on used car must be above 1000");
            }

            vehicle.VINNumber = model.VINNumber;

            vehicle.MSRP = model.MSRP;

            if (vehicle.MSRP < 0)
            {
                ModelState.AddModelError("MSRP", "MSRP must be a positive number");
            }

            vehicle.SalesPrice = model.SalesPrice;

            if (vehicle.SalesPrice < 0)
            {
                ModelState.AddModelError("SalesPrice", "Sales price must be a positive number");
            }

            vehicle.VehicleDescription = model.VehicleDescription;

            vehicle.IsFeaturedVehicle = null;

            if (model.UploadedFile != null)
            {
                if (model.UploadedFile.ContentType != "image/jpeg" && model.UploadedFile.ContentType != "image/jpg" && model.UploadedFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("UploadedFile", "Please upload a Jpeg, Jpg, or Png file");
                }
            }

            if (ModelState.IsValid)
            { 
                repo.AddVehicle(vehicle);

                if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
                {
                    if (model.UploadedFile.ContentType == "image/jpeg")
                    {
                        vehicle.ImageFileName = "inventory-" + vehicle.VehicleID + ".jpeg";
                        string path = Path.Combine(Server.MapPath("~/images"), "inventory-" + vehicle.VehicleID + ".jpeg");
                        model.UploadedFile.SaveAs(path);
                        repo.UpdateVehicle(vehicle);
                    }
                    if (model.UploadedFile.ContentType == "image/png")
                    {
                        vehicle.ImageFileName = "inventory-" + vehicle.VehicleID + ".png";
                        string path = Path.Combine(Server.MapPath("~/images"), "inventory-" + vehicle.VehicleID + ".png");
                        model.UploadedFile.SaveAs(path);
                        repo.UpdateVehicle(vehicle);
                    }
                    if (model.UploadedFile.ContentType == "image/jpg")
                    {
                        vehicle.ImageFileName = "inventory-" + vehicle.VehicleID + ".jpg";
                        string path = Path.Combine(Server.MapPath("~/images"), "inventory-" + vehicle.VehicleID + ".jpg");
                        model.UploadedFile.SaveAs(path);
                        repo.UpdateVehicle(vehicle);
                    }
                    
                }

                return RedirectToAction("Vehicles");
            }
            else
            {
                var mRepo = CarMakesRepositoryFactory.GetRepository();

                List<CarMakes> makes = mRepo.GetAll();

                var carMakeNames = (from carmake in makes
                                    select carmake.MakeName).ToList();

                model.CarMakeNames = new SelectList(carMakes, "MakeName", "MakeName", 1);

                return View("AddVehicle", model);
            }
            
        }
        [HttpGet]
        public ActionResult EditVehicle(int id)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            EditVehicleViewModel vm = new EditVehicleViewModel();

            var vehicle = repo.GetByID(id);

            vm.VehicleID = id;

            vm.MakeID = vehicle.MakeID;

            vm.ModelID = vehicle.ModelID;

            var makeRepo = CarMakesRepositoryFactory.GetRepository();

            List<CarMakes> carMakes = makeRepo.GetAll();

            vm.CarMakeNames = new SelectList(carMakes, "MakeName", "MakeName", 1);

            var makeID = (from make in carMakes
                          where vm.CarMakeName == make.MakeName
                          select make.MakeID).FirstOrDefault();

            vehicle.MakeID = makeID;

            var modelRepo = CarModelItemRepositoryFactory.GetRepository();

            List<CarModelItem> carModels = modelRepo.GetAll();

            var modelName = (from carmodel in carModels
                             where vm.MakeID == carmodel.MakeID
                             select carmodel.ModelName).FirstOrDefault();

            vm.ModelName = modelName;

            vm.SalesTypeID = vehicle.SalesTypeID;

            vm.BodyTypeID = vehicle.BodyTypeID;

            vm.YearBuilt = vehicle.YearBuilt;

            vm.TransmissionID = vehicle.TransmissionID;

            vm.ExtColorID = vehicle.ExtColorID;

            vm.IntColorID = vehicle.IntColorID;

            vm.Mileage = vehicle.Mileage;

            vm.VINNumber = vehicle.VINNumber;

            vm.MSRP = vehicle.MSRP;

            vm.SalesPrice = vehicle.SalesPrice;

            vm.VehicleDescription = vehicle.VehicleDescription;

            vm.ImageFileName = vehicle.ImageFileName;

            return View(vm);

        }

        [HttpPost]
        public ActionResult EditVehicle(EditVehicleViewModel model)
        {
            VehicleInventory vehicle = new VehicleInventory();

            var repo = VehicleRepositoryFactory.GetRepository();

            vehicle.VehicleID = model.VehicleID;

            var makeRepo = CarMakesRepositoryFactory.GetRepository();

            List<CarMakes> carMakes = makeRepo.GetAll();

            var makeID = (from make in carMakes
                          where model.CarMakeName == make.MakeName
                          select make.MakeID).FirstOrDefault();

            vehicle.MakeID = makeID;

            var modelRepo = CarModelItemRepositoryFactory.GetRepository();

            List<CarModelItem> carModels = modelRepo.GetAll();

            var modelID = (from carmodel in carModels
                           where model.ModelName == carmodel.ModelName
                           select carmodel.ModelID).FirstOrDefault();

            vehicle.ModelID = modelID;

            vehicle.SalesTypeID = model.SalesTypeID;

            vehicle.BodyTypeID = model.BodyTypeID;

            vehicle.YearBuilt = model.YearBuilt;

            vehicle.TransmissionID = model.TransmissionID;

            vehicle.ExtColorID = model.ExtColorID;

            vehicle.IntColorID = model.IntColorID;

            vehicle.Mileage = model.Mileage;

            if (vehicle.SalesTypeID == 1 && vehicle.Mileage > 1000)
            {
                ModelState.AddModelError("Mileage", "Mileage on new car must be below 1000");
            }
            if (vehicle.SalesTypeID == 2 && vehicle.Mileage < 1000)
            {
                ModelState.AddModelError("Mileage", "Mileage on used car must be above 1000");
            }

            vehicle.VINNumber = model.VINNumber;

            vehicle.MSRP = model.MSRP;

            if (vehicle.MSRP < 0)
            {
                ModelState.AddModelError("MSRP", "MSRP must be a positive number");
            }

            vehicle.SalesPrice = model.SalesPrice;

            if (vehicle.SalesPrice < 0)
            {
                ModelState.AddModelError("SalesPrice", "Sales price must be a positive number");
            }

            vehicle.VehicleDescription = model.VehicleDescription;

            if (model.IsFeaturedVehicle == true)
            {
                vehicle.IsFeaturedVehicle = true;
            }
            else
            {
                vehicle.IsFeaturedVehicle = false;
            }

            if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
            {
                if (model.UploadedFile.ContentType == "image/jpeg")
                {
                    vehicle.ImageFileName = "inventory-" + vehicle.VehicleID + ".jpeg";
                    string path = Path.Combine(Server.MapPath("~/images"), "inventory-" + vehicle.VehicleID + ".jpeg");
                    model.UploadedFile.SaveAs(path);
                }
                if (model.UploadedFile.ContentType == "image/png")
                {
                    vehicle.ImageFileName = "inventory-" + vehicle.VehicleID + ".png";
                    string path = Path.Combine(Server.MapPath("~/images"), "inventory-" + vehicle.VehicleID + ".png");
                    model.UploadedFile.SaveAs(path);

                }
                if (model.UploadedFile.ContentType == "image/jpg")
                {
                    vehicle.ImageFileName = "inventory-" + vehicle.VehicleID + ".jpg";
                    string path = Path.Combine(Server.MapPath("~/images"), "inventory-" + vehicle.VehicleID + ".jpg");
                    model.UploadedFile.SaveAs(path);
                }
                if (model.UploadedFile.ContentType != "image/jpg" && model.UploadedFile.ContentType != "image/jpeg" && model.UploadedFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("UploadedFile", "Please upload only a Jpeg, Jpg, or Png file");
                } 
            }
            else
            {
                vehicle.ImageFileName = model.ImageFileName;
            }

            if (ModelState.IsValid)
            {
                repo.UpdateVehicle(vehicle);

                return RedirectToAction("Vehicles");
            }
            else
            {
                var car = repo.GetByID(model.VehicleID);

                var maRepo = CarMakesRepositoryFactory.GetRepository();

                List<CarMakes> cMakes = maRepo.GetAll();

                var maID = (from make in cMakes
                              where model.CarMakeName == make.MakeName
                              select make.MakeID).FirstOrDefault();

                model.MakeID = maID;

                var moRepo = CarModelItemRepositoryFactory.GetRepository();

                List<CarModelItem> cModels = moRepo.GetAll();

                var moID = (from carmodel in cModels
                               where model.ModelName == carmodel.ModelName
                               select carmodel.ModelID).FirstOrDefault();

                model.ModelID = moID;

                model.SalesTypeID = car.SalesTypeID;

                model.BodyTypeID = car.BodyTypeID;

                model.YearBuilt = car.YearBuilt;

                model.TransmissionID = car.TransmissionID;

                model.ExtColorID = car.ExtColorID;

                model.IntColorID = car.IntColorID;

                model.Mileage = car.Mileage;

                model.VINNumber = car.VINNumber;

                model.MSRP = car.MSRP;

                model.SalesPrice = car.SalesPrice;

                model.VehicleDescription = car.VehicleDescription;

                model.ImageFileName = car.ImageFileName;

                var mRepo = CarMakesRepositoryFactory.GetRepository();

                List<CarMakes> makes = mRepo.GetAll();

                model.CarMakeNames = new SelectList(carMakes, "MakeName", "MakeName", 1);

                return View("EditVehicle", model);
            }
           
        }

        public ActionResult Makes()
        {
            CarMakeItem make = new CarMakeItem();

            return View(make);
        }

        [HttpPost]
        public ActionResult InsertCarMake(CarMakes model)
        {
            CarMakes make = new CarMakes();

            var makeRepo = CarMakesRepositoryFactory.GetRepository();

            var repo = CarMakesRepositoryFactory.GetRepository();

            var carMakes = makeRepo.GetAll();

            make.MakeName = model.MakeName;

            DateTime today = DateTime.Today;

            make.DateAdded = today;

            make.Email = User.Identity.Name;

            if (make.MakeName == null)
            {
                ModelState.AddModelError("MakeName", "Make name cannot be blank");
            }

            if (ModelState.IsValid)
            {
                repo.Insert(make);

                return View("Makes");
            }
            else
            {
                return View("Makes");
            }
            

        }

        public ActionResult Models()
        {
            CarModelItemViewModel vm = new CarModelItemViewModel();

            var makeRepo = CarMakesRepositoryFactory.GetRepository();

            List<CarMakes> carMakes = makeRepo.GetAll();

            var carMakeNames = (from carmake in carMakes
                                select carmake.MakeName).ToList();

            vm.CarMakeNames = new SelectList(carMakes, "MakeName", "MakeName", 1);

            return View(vm);
        }

        [HttpPost]
        public ActionResult InsertCarModel(CarModelItemViewModel model)
        {
            CarModels carmodel = new CarModels();

            var makeRepo = CarMakesRepositoryFactory.GetRepository();

            var repo = CarModelsRepositoryFactory.GetRepository();

            var carModels = makeRepo.GetAll();

            var makeID = (from carmod in carModels
                          where model.MakeName == carmod.MakeName
                          select carmod.MakeID).FirstOrDefault();

            carmodel.MakeID = makeID;

            carmodel.ModelName = model.ModelName;

            DateTime today = DateTime.Today;

            carmodel.DateAdded = today;

            carmodel.Email = User.Identity.Name;

            if (model.ModelName == null)
            {
                ModelState.AddModelError("ModelName", "Model name cannot be blank");
            }

            if (model.MakeName == null)
            {
                ModelState.AddModelError("MakeName", "Make name cannot be blank");
            }

            if (ModelState.IsValid)
            {
                var mRepo = CarMakesRepositoryFactory.GetRepository();

                List<CarMakes> carMakes = mRepo.GetAll();

                var carMakeNames = (from carmake in carMakes
                                    select carmake.MakeName).ToList();

                model.CarMakeNames = new SelectList(carMakes, "MakeName", "MakeName", 1);

                repo.Insert(carmodel);

                return View("Models", model);
            }
            else
            {

                var mRepo = CarMakesRepositoryFactory.GetRepository();

                List<CarMakes> carMakes = mRepo.GetAll();

                var carMakeNames = (from carmake in carMakes
                                    select carmake.MakeName).ToList();

                model.CarMakeNames = new SelectList(carMakes, "MakeName", "MakeName", 1);

                return View("Models", model);
            }

        }

        [Authorize(Roles = "admin")]
        public ActionResult Specials()
        {
            var model = new SalesSpecials();

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult InsertSpecial(SalesSpecials special)
        {
            if (special.SpecialName == null)
            {
                ModelState.AddModelError("SpecialName", "Special name cannot be blank");
            }
            if (special.SpecialDescription == null)
            {
                ModelState.AddModelError("SpecialDescription", "Special description cannot be blank");
            }

            var repo = SalesSpecialsRepositoryFactory.GetRepository();

            if (ModelState.IsValid)
            {
                repo.InsertSpecial(special);

                return RedirectToAction("Specials");
            }
            return View("Specials", special);           
        }

        [Authorize(Roles = "admin")]
        public ActionResult Users()
        {
            var repo = UsersRepositoryFactory.GetRepository();

            var model = repo.GetAll();

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddUser()
        {
            UserViewModel model = new UserViewModel();

            var repo = UsersRepositoryFactory.GetRepository();

            List<UserRoles> roles = repo.GetUserRoles();

            var userRoles = (from role in roles
                                select role.Name).ToList();

            model.Roles = new SelectList(userRoles);

            return View(model);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddUser(UserViewModel model, GuildCarsDbContext context)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleMgr = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            var user = new ApplicationUser();
            
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.Email;
            user.Email = model.Email;

            if (ModelState.IsValid)
            {
                userMgr.Create(user, model.Password);
                userMgr.AddToRole(user.Id, model.Role);
                return RedirectToAction("Users");
            }
            else
            {
                var repo = UsersRepositoryFactory.GetRepository();

                List<UserRoles> roles = repo.GetUserRoles();

                var userRoles = (from role in roles
                                 select role.Name).ToList();

                model.Roles = new SelectList(userRoles);

                return View("AddUser", model);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditUser(string id)
        {
            EditUserViewModel model = new EditUserViewModel();

            var repo = UsersRepositoryFactory.GetRepository();

            var users = repo.GetAll();

            var appUser = (from u in users
                           where id == u.Id
                           select u).FirstOrDefault();


            model.Id = appUser.Id;

            model.FirstName = appUser.FirstName;

            model.LastName = appUser.LastName;

            model.Email = appUser.Email;

            List<UserRoles> roles = repo.GetUserRoles();

            var userRoles = (from role in roles
                             select role.Name).ToList();

            model.Roles = new SelectList(userRoles);

            return View(model);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditUser(EditUserViewModel model, GuildCarsDbContext context)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleMgr = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            ApplicationUser user = userMgr.FindById(model.Id);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserName = model.Email;

            var repo = UsersRepositoryFactory.GetRepository();

            List<UserRoles> roles = repo.GetUserRoles();

            List<Users> users = repo.GetAll();

            var newUserRole = (from role in roles
                            where model.Role == role.Name
                            select role.Name).FirstOrDefault();

            var oldUserRole = (from u in users
                               where model.FirstName == user.FirstName
                               select u.Role).FirstOrDefault();
            
            if (ModelState.IsValid)
            {
                userMgr.RemoveFromRole(user.Id, oldUserRole);
                userMgr.AddToRole(user.Id, model.Role);
                userMgr.Update(user);

                return RedirectToAction("Users");
            }
            else
            {
                List<UserRoles> rolenames = repo.GetUserRoles();

                var userRoles = (from role in rolenames
                                 select role.Name).ToList();

                model.Roles = new SelectList(userRoles);

                return View("EditUser", model);
            }
        }

        [Authorize(Roles = "admin,sales")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ChangePasswordAsync(ChangePasswordViewModel model, GuildCarsDbContext context)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var result = await userMgr.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            return RedirectToAction("Index", "Home");
            
        }

        [Authorize(Roles = "admin")]
        public ActionResult Reports()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult InventoryReport()
        {
            var repo = VehicleInventoryReportRepositoryFactory.GetRepository();

            dynamic mymodel = new ExpandoObject();

            mymodel.NewVehicles = repo.GetNewInventoryReport();

            mymodel.UsedVehicles = repo.GetUsedInventoryReport();

            return View(mymodel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult SalesReport()
        {
            var repo = SalesInformationRepositoryFactory.GetRepository();

            SalesReportViewModel model = new SalesReportViewModel();

            var userRepo = UsersRepositoryFactory.GetRepository();

            var users = userRepo.GetAll();

            var usernames = (from user in users
                             select user.LastName);

            model.Users = new SelectList(usernames);

            return View(model);
        }
    }
}
 