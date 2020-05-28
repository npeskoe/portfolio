using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
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
    public class VehicleInventoryReportRepositoryDapper : IVehicleInventoryReportRepository
    {
        public List<VehicleInventoryReport> GetNewInventoryReport()
        {
            List<VehicleInventoryReport> newInventory = new List<VehicleInventoryReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                newInventory = cn.Query<VehicleInventoryReport>("NewVehicleInventoryReportSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return newInventory;
        }

        public List<VehicleInventoryReport> GetUsedInventoryReport()
        {
            List<VehicleInventoryReport> usedInventory = new List<VehicleInventoryReport>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                usedInventory = cn.Query<VehicleInventoryReport>("UsedVehicleInventoryReportSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return usedInventory;
        }
    }
}
