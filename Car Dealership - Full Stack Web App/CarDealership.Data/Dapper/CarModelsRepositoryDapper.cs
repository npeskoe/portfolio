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
    public class CarModelRepositoryDaper : ICarModelsRepository
    {
        public void Insert(CarModels model)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@ModelID",
                    dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@MakeID", model.MakeID);
                parameters.Add("@ModelName", model.ModelName);
                parameters.Add("@DateAdded", model.DateAdded);
                parameters.Add("@Email", model.Email);
                
                cn.Execute("ModelInsert",
                    parameters, commandType: CommandType.StoredProcedure);

                model.ModelID = parameters.Get<int>("@ModelID");
            }
        }
    }
}
