using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace CarDealership.Data.Dapper
{
    public class BodyTypesRepositoryDapper : IBodyTypesRepository
    {
        public List<BodyTypes> GetAll()
        {
            List<BodyTypes> bodyTypes = new List<BodyTypes>();

            using(var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                bodyTypes = cn.Query<BodyTypes>("BodyTypesSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return bodyTypes;
        }
    }
}
