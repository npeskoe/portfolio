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
    public class CarMakesRepositoryDapper : ICarMakesRepository
    {
        public List<CarMakes> GetAll()
        {
            List<CarMakes> carMakes = new List<CarMakes>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                carMakes = cn.Query<CarMakes>("CarMakesSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return carMakes;
        }

        public void Insert(CarMakes make)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@MakeID",
                    dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@MakeName", make.MakeName);
                parameters.Add("@DateAdded", make.DateAdded);
                parameters.Add("@Email", make.Email);

                cn.Execute("MakeInsert",
                    parameters, commandType: CommandType.StoredProcedure);

                make.MakeID = parameters.Get<int>("@MakeID");
            }
        }
    }
}
