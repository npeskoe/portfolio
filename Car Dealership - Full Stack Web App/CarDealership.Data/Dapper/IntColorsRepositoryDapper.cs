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
    public class IntColorsRepositoryDapper : IIntColorsRepository
    {

        public List<IntColors> GetAll()
        {
            List<IntColors> intColors = new List<IntColors>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                intColors = cn.Query<IntColors>("IntColorsSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return intColors;
        }
    }
}
