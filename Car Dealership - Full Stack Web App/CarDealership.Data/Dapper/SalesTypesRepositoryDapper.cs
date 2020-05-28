using CarDealership.Data.Interfaces;
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
    public class SalesTypesRepositoryDapper : ISalesTypeRepository
    {
        public List<SalesTypes> GetAll()
        {
            List<SalesTypes> salesTypes = new List<SalesTypes>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                salesTypes = cn.Query<SalesTypes>("SalesTypesSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return salesTypes;
        }
    }
}
