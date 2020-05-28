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
    public class SalesSpecialsRepositoryDapper : ISalesSpecialRepository
    {
        public List<SalesSpecials> GetAllSpecials()
        {
            List<SalesSpecials> salesSpecials = new List<SalesSpecials>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                salesSpecials = cn.Query<SalesSpecials>("SpecialsSelect", commandType: CommandType.StoredProcedure).ToList();
            }

            return salesSpecials;

        }

        public void InsertSpecial(SalesSpecials special)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@SpecialID",
                    dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@SpecialName", special.SpecialName);
                parameters.Add("@SpecialDescription", special.SpecialDescription);

                cn.Execute("SpecialInsert",
                    parameters, commandType: CommandType.StoredProcedure);

                special.SpecialID = parameters.Get<int>("@SpecialID");
            }
        }

        public void DeleteSpecial(int specialID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SpecialID", specialID);

                cn.Execute("SpecialDelete",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
        }
        public SalesSpecials GetSpecialByID(int specialID)
        {

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@SpecialID", specialID);

                return cn.Query<SalesSpecials>(
                    "SpecialSelect",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

        }
    }
}
