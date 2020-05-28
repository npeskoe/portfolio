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
    public class ContactUsRepositoryDapper : IContactUsRepository
    {
        public void Insert(ContactUsRequest request)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@ContactUsID",
                    dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@ContactName", request.ContactName);
                parameters.Add("@ContactEmail", request.ContactEmail);
                parameters.Add("@ContactPhone", request.ContactPhone);
                parameters.Add("@ContactMessage", request.ContactMessage);

                cn.Execute("ContactUsInsert",
                    parameters, commandType: CommandType.StoredProcedure);

                request.ContactUsID = parameters.Get<int>("@ContactUsID");
            }
        }
    }
}
