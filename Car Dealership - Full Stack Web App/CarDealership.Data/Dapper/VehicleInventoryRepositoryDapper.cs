using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Dapper
{
    public class VehicleInventoryRepositoryDapper : IVehicleInventoryRepository
    {
        public void DeleteVehicle(int vehicleID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@VehicleID", vehicleID);

                cn.Execute("VehicleDelete",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public List<VehicleInventory> GetAll()
        {
            List<VehicleInventory> vehicleInventory = new List<VehicleInventory>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                vehicleInventory = cn.Query<VehicleInventory>("VehicleInventorySelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return vehicleInventory;
        }

        public VehicleInventory GetByID(int vehicleID)
        {

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@VehicleID", vehicleID);

                return cn.Query<VehicleInventory>(
                    "VehicleSelect",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

        }

        public IEnumerable<FeaturedVehicles> GetFeatured()
        {
            List<FeaturedVehicles> featuredVehicleInventory = new List<FeaturedVehicles>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                featuredVehicleInventory = cn.Query<FeaturedVehicles>("FeaturedVehicleSelect", commandType: CommandType.StoredProcedure).ToList();
            }

            return featuredVehicleInventory;
        }

        public VehicleItem GetVehicleDetails(int vehicleID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@VehicleID", vehicleID);

                var vehicle = cn.Query<VehicleItem>(
                    "VehicleSelectDetails",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault(); ;

                return vehicle;
            }
        }

        public IEnumerable<VehicleItem> GetVehicleItems()
        {
            List<VehicleItem> vehicleItems = new List<VehicleItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                vehicleItems = cn.Query<VehicleItem>("VehicleItemsSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return vehicleItems;
        }

        public void AddVehicle(VehicleInventory vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@VehicleID",
                    dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@ModelID", vehicle.ModelID);
                parameters.Add("@MakeID", vehicle.MakeID);
                parameters.Add("@SalesTypeID", vehicle.SalesTypeID);
                parameters.Add("@BodyTypeID", vehicle.BodyTypeID);
                parameters.Add("@YearBuilt", vehicle.YearBuilt);
                parameters.Add("@TransmissionID", vehicle.TransmissionID);
                parameters.Add("@ExtColorID", vehicle.ExtColorID);
                parameters.Add("@IntColorID", vehicle.IntColorID);
                parameters.Add("@Mileage", vehicle.Mileage);
                parameters.Add("@VINNumber", vehicle.VINNumber);
                parameters.Add("@MSRP", vehicle.MSRP);
                parameters.Add("@SalesPrice", vehicle.SalesPrice);
                parameters.Add("@VehicleDescription", vehicle.VehicleDescription);
                parameters.Add("@IsFeaturedVehicle", vehicle.IsFeaturedVehicle);
                parameters.Add("@ImageFileName", vehicle.ImageFileName);

                cn.Execute("VehicleInsert",
                    parameters, commandType: CommandType.StoredProcedure);

                vehicle.VehicleID = parameters.Get<int>("@VehicleID");
            }
        }

        public void UpdateVehicle(VehicleInventory vehicle)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@VehicleID", vehicle.VehicleID);
                parameters.Add("@ModelID", vehicle.ModelID);
                parameters.Add("@MakeID", vehicle.MakeID);
                parameters.Add("@SalesTypeID", vehicle.SalesTypeID);
                parameters.Add("@BodyTypeID", vehicle.BodyTypeID);
                parameters.Add("@YearBuilt", vehicle.YearBuilt);
                parameters.Add("@TransmissionID", vehicle.TransmissionID);
                parameters.Add("@ExtColorID", vehicle.ExtColorID);
                parameters.Add("@IntColorID", vehicle.IntColorID);
                parameters.Add("@Mileage", vehicle.Mileage);
                parameters.Add("@VINNumber", vehicle.VINNumber);
                parameters.Add("@MSRP", vehicle.MSRP);
                parameters.Add("@SalesPrice", vehicle.SalesPrice);
                parameters.Add("@VehicleDescription", vehicle.VehicleDescription);
                parameters.Add("@IsFeaturedVehicle", vehicle.IsFeaturedVehicle);
                parameters.Add("@ImageFileName", vehicle.ImageFileName);
                cn.Execute("VehicleUpdate",
                    parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<VehicleItem> GetNewInventory()
        {
            List<VehicleItem> newInventory = new List<VehicleItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                newInventory = cn.Query<VehicleItem>("NewVehiclesSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return newInventory;
        }

        public IEnumerable<VehicleItem> GetUsedInventory()
        {
            List<VehicleItem> usedInventory = new List<VehicleItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                usedInventory = cn.Query<VehicleItem>("UsedVehiclesSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return usedInventory;
        }
        
    }
}
