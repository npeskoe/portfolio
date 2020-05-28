using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IVehicleInventoryReportRepository
    {
        List<VehicleInventoryReport> GetNewInventoryReport();
        List<VehicleInventoryReport> GetUsedInventoryReport();
    }
}
