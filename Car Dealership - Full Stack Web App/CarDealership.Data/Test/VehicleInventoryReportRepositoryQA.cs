using CarDealership.Data.Factory;
using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    class VehicleInventoryReportRepositoryQA : IVehicleInventoryReportRepository
    {
        public List<VehicleInventoryReport> GetNewInventoryReport()
        {
            List<VehicleInventoryReport> reports = new List<VehicleInventoryReport>();

            var repo = VehicleRepositoryFactory.GetRepository();

            var vehicles = repo.GetAll();

            var salesRepo = SalesInformationRepositoryFactory.GetRepository();

            var sales = salesRepo.GetSales();

            var salesTypeRepo = SalesTypeRepositoryFactory.GetRepository();

            var salesTypes = salesTypeRepo.GetAll();

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
                        join sale in salesTypes on v.SalesTypeID equals sale.SalesTypeID
                        join make in makes on v.MakeID equals make.MakeID
                        join model in models on v.ModelID equals model.ModelID
                        join s in sales on v.VehicleID equals s.VehicleID
                        where v.SalesTypeID == 1
                        select new VehicleInventoryReport
                        {
                            VehicleID = v.VehicleID,
                            SalesTypeID = v.SalesTypeID,
                            YearBuilt = v.YearBuilt,
                            MakeName = make.MakeName,
                            ModelName = model.ModelName,
                            Count = sales.Select(x => x.SalesID).Count(),
                            StockValue = vehicles.Sum(x => x.MSRP)
                        };

            List<VehicleInventoryReport> report = items.ToList();

            return report;
        }

        public List<VehicleInventoryReport> GetUsedInventoryReport()
        {
            List<VehicleInventoryReport> reports = new List<VehicleInventoryReport>();

            var repo = VehicleRepositoryFactory.GetRepository();

            var vehicles = repo.GetAll();

            var salesRepo = SalesInformationRepositoryFactory.GetRepository();

            var sales = salesRepo.GetSales();

            var salesTypeRepo = SalesTypeRepositoryFactory.GetRepository();

            var salesTypes = salesTypeRepo.GetAll();

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
                        join sale in salesTypes on v.SalesTypeID equals sale.SalesTypeID
                        join make in makes on v.MakeID equals make.MakeID
                        join model in models on v.ModelID equals model.ModelID
                        join s in sales on v.VehicleID equals s.VehicleID
                        where v.SalesTypeID == 2
                        select new VehicleInventoryReport
                        {
                            VehicleID = v.VehicleID,
                            SalesTypeID = v.SalesTypeID,
                            YearBuilt = v.YearBuilt,
                            MakeName = make.MakeName,
                            ModelName = model.ModelName,
                            Count = sales.Select(x => x.SalesID).Count(),
                            StockValue = vehicles.Sum(x => x.MSRP)
                        };

            List<VehicleInventoryReport> report = items.ToList();

            return report;
        }
    }
}
