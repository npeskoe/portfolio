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
    public class ExtColorsRepositoryDapper : IExtColorsRepository
    {

        public List<ExtColors> GetAll()
        {
            List<ExtColors> extColors = new List<ExtColors>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                extColors = cn.Query<ExtColors>("ExtColorsSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return extColors;
        }
    }
}
