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
    public class CarModelItemRepositoryDapper : ICarModelItemRepository
    {
        public List<CarModelItem> GetAll()
        {
            List<CarModelItem> carModels = new List<CarModelItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                carModels = cn.Query<CarModelItem>("CarModelItemSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return carModels;
        }

        public List<CarModelItem> GetModelsByMake(string makeName)
        {
            List<CarModelItem> models = new List<CarModelItem>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                models = cn.Query<CarModelItem>("CarModelItemSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            var modeslByMake = models.Where(m => m.MakeName == makeName).ToList();

            return modeslByMake;
        }

    }
}
