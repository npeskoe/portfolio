using CarDealership.Data.Factory;
using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    class VehicleInventoryRepositoryQA : IVehicleInventoryRepository
    {

        public List<VehicleInventory> GetAll()
        {
            List<VehicleInventory> inventory = new List<VehicleInventory>();

            inventory.Add(new VehicleInventory { VehicleID = 1, MakeID = 1, ModelID = 1, SalesTypeID = 2, BodyTypeID = 1, YearBuilt = 2019, TransmissionID = 1, ExtColorID = 5, IntColorID = 2, Mileage = 51243, VINNumber = "5J8TB4H3XHL018174", MSRP = 19500, SalesPrice = 19000, VehicleDescription = "Gently used MDX", ImageFileName = "placeholder.png", IsFeaturedVehicle = true });
            inventory.Add(new VehicleInventory { VehicleID = 2, MakeID = 1, ModelID = 2, SalesTypeID = 1, BodyTypeID = 2, YearBuilt = 2020, TransmissionID = 1, ExtColorID = 1, IntColorID = 2, Mileage = 900, VINNumber = "2CNALDEC6B6245011", MSRP = 26900, SalesPrice = 25000, VehicleDescription = "Brand new RDX", ImageFileName = "placeholder.png", IsFeaturedVehicle = null });
            inventory.Add(new VehicleInventory { VehicleID = 3, MakeID = 2, ModelID = 3, SalesTypeID = 2, BodyTypeID = 1, YearBuilt = 2016, TransmissionID = 1, ExtColorID = 3, IntColorID = 1, Mileage = 69198, VINNumber = "JNKCV64EX8M190416", MSRP = 23900, SalesPrice = 22000, VehicleDescription = "Like new A3", ImageFileName = "placeholder.png", IsFeaturedVehicle = null });

            return inventory;
        }

        public void AddVehicle(VehicleInventory vehicle)
        {
            var repo = GetAll();

            VehicleInventory newVehicle = new VehicleInventory();

            newVehicle.MakeID = vehicle.MakeID;
            newVehicle.ModelID = vehicle.ModelID;
            newVehicle.SalesTypeID = vehicle.SalesTypeID;
            newVehicle.BodyTypeID = vehicle.BodyTypeID;
            newVehicle.YearBuilt = vehicle.YearBuilt;
            newVehicle.TransmissionID = vehicle.TransmissionID;
            newVehicle.ExtColorID = vehicle.ExtColorID;
            newVehicle.IntColorID = vehicle.IntColorID;
            newVehicle.Mileage = vehicle.Mileage;
            newVehicle.VINNumber = vehicle.VINNumber;
            newVehicle.MSRP = vehicle.MSRP;
            newVehicle.SalesPrice = vehicle.SalesPrice;
            newVehicle.VehicleDescription = vehicle.VehicleDescription;
            newVehicle.IsFeaturedVehicle = vehicle.IsFeaturedVehicle;
            newVehicle.ImageFileName = vehicle.ImageFileName;

        }

        public VehicleInventory GetByID(int vehicleID)
        {
            var repo = GetAll();

            var vehicle = repo.Where(v => v.VehicleID == vehicleID).FirstOrDefault();

            return vehicle;
        }

        public void DeleteVehicle(int vehicleID)
        {
            var repo = GetAll();

            var vehicle = GetByID(vehicleID);

            repo.Remove(vehicle);
        }

        public IEnumerable<FeaturedVehicles> GetFeatured()
        {
            var repo = GetAll();

            var makeRepo = CarMakesRepositoryFactory.GetRepository();

            var makes = makeRepo.GetAll();

            var modelRepo = CarModelItemRepositoryFactory.GetRepository();

            var models = modelRepo.GetAll();


            var featured = from v in repo
                           join make in makes on v.MakeID equals make.MakeID
                           join model in models on v.ModelID equals model.ModelID
                           where v.IsFeaturedVehicle == true
                           select new FeaturedVehicles
                           {
                               VehicleID = v.VehicleID,
                               ImageFileName = v.ImageFileName,
                               YearBuilt = v.YearBuilt,
                               MakeName = make.MakeName,
                               ModelName = model.ModelName,
                               SalesPrice = v.SalesPrice
                           };

            return featured;
        }

        public IEnumerable<VehicleItem> GetNewInventory()
        {
            var repo = GetVehicleItems();

            var newVehicles = repo.Where(v => v.SalesTypeID == 1).ToList();

            return newVehicles;
        }

        public IEnumerable<VehicleItem> GetVehicleItems()
        {
            List<VehicleItem> vehicleItems = new List<VehicleItem>();

            var repo = VehicleRepositoryFactory.GetRepository();

            var vehicles = repo.GetAll();

            var salesRepo = SalesInformationRepositoryFactory.GetRepository();

            var sales = salesRepo.GetSales();

            var bodyRepo = BodyTypesRepositoryFactory.GetRepository();

            var bodyTypes = bodyRepo.GetAll();

            var makeRepo = CarMakesRepositoryFactory.GetRepository();

            var makes = makeRepo.GetAll();

            var modelRepo = CarModelItemRepositoryFactory.GetRepository();

            var models = modelRepo.GetAll();

            var transRepo = TransmissionTypesRepositoryFactory.GetRepository();

            var transTypes = transRepo.GetAll();

            var extColorRepo = ExtColorsRepositoryFactory.GetRepository();

            var extColors = extColorRepo.GetAll();

            var intColorRepo = IntColorsRepositoryFactory.GetRepository();

            var intColors = intColorRepo.GetAll();

            var items = from v in vehicles
                                      join make in makes on v.MakeID equals make.MakeID
                                      join model in models on v.ModelID equals model.ModelID
                                      join body in bodyTypes on v.BodyTypeID equals body.BodyTypeID
                                      join tran in transTypes on v.TransmissionID equals tran.TransmissionID
                                      join ext in extColors on v.ExtColorID equals ext.ExtColorID
                                      join color in intColors on v.IntColorID equals color.IntColorID
                                      select new VehicleItem {
                                          VehicleID = v.VehicleID,
                                          SalesTypeID = v.SalesTypeID,
                                          YearBuilt = v.YearBuilt,
                                          MakeName = make.MakeName,
                                          ModelName = model.ModelName,
                                          BodyTypeName = body.BodyTypeName,
                                          TransmissionTypeName = tran.TransmissionTypeName,
                                          ExtColorName = ext.ExtColorName,
                                          IntColorName = color.IntColorName,
                                          Mileage = v.Mileage,
                                          VINNumber = v.VINNumber,
                                          SalesPrice = v.SalesPrice,
                                          MSRP = v.MSRP,
                                          VehicleDescription = v.VehicleDescription,
                                          ImageFileName = v.ImageFileName
                                      };

            List<VehicleItem> vitems = items.ToList();

            return vitems;
        }

        public IEnumerable<VehicleItem> GetUsedInventory()
        {
            var repo = GetVehicleItems();

            var usedVehicles = repo.Where(v => v.SalesTypeID == 2).ToList();

            return usedVehicles;
        }

        public VehicleItem GetVehicleDetails(int vehicleID)
        {
            var repo = GetVehicleItems();

            var vehicleItem = repo.Where(V => V.VehicleID == vehicleID).FirstOrDefault();

            return vehicleItem;
        }

        public void UpdateVehicle(VehicleInventory vehicle)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            VehicleInventory updateVehicle = new VehicleInventory();

            updateVehicle.MakeID = vehicle.MakeID;
            updateVehicle.ModelID = vehicle.ModelID;
            updateVehicle.SalesTypeID = vehicle.SalesTypeID;
            updateVehicle.BodyTypeID = vehicle.BodyTypeID;
            updateVehicle.YearBuilt = vehicle.YearBuilt;
            updateVehicle.TransmissionID = vehicle.TransmissionID;
            updateVehicle.ExtColorID = vehicle.ExtColorID;
            updateVehicle.IntColorID = vehicle.IntColorID;
            updateVehicle.Mileage = vehicle.Mileage;
            updateVehicle.VINNumber = vehicle.VINNumber;
            updateVehicle.MSRP = vehicle.MSRP;
            updateVehicle.SalesPrice = vehicle.SalesPrice;
            updateVehicle.VehicleDescription = vehicle.VehicleDescription;
            updateVehicle.IsFeaturedVehicle = vehicle.IsFeaturedVehicle;
            updateVehicle.ImageFileName = vehicle.ImageFileName;

            repo.UpdateVehicle(updateVehicle);

        }
    }
}
