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
    public class TransmissionTypesRepositoryDapper : ITransmissionTypesRepository
    {
        public List<TransmissionTypes> GetAll()
        {
            List<TransmissionTypes> transmissionTypes = new List<TransmissionTypes>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                transmissionTypes = cn.Query<TransmissionTypes>("TransmissionTypesSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return transmissionTypes;
        }
    }
}
