using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IVehicleInventoryRepository
    {
        List<VehicleInventory> GetAll();
        VehicleInventory GetByID(int vehicleID);
        void AddVehicle(VehicleInventory vehicle);
        void UpdateVehicle(VehicleInventory vehicle);
        void DeleteVehicle(int vehicleID);
        IEnumerable<FeaturedVehicles> GetFeatured();
        IEnumerable<VehicleItem> GetVehicleItems();
        VehicleItem GetVehicleDetails(int vehicleID);
        IEnumerable<VehicleItem> GetNewInventory();
        IEnumerable<VehicleItem> GetUsedInventory();
    }
}
