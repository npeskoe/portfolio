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
    public class SalesInformationRepositoryDapper : ISalesInformationRepository
    {
        public List<SalesInformation> GetSales()
        {
            List<SalesInformation> sales = new List<SalesInformation>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                sales = cn.Query<SalesInformation>("SalesInformationSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return sales;
        }

        public void Insert(SalesInformation sale)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@SalesID",
                    dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@CustomerName", sale.CustomerName);
                parameters.Add("@CustomerPhone", sale.CustomerPhone);
                parameters.Add("@CustomerEmail", sale.CustomerEmail);
                parameters.Add("@CustomerStreet1", sale.CustomerStreet1);
                parameters.Add("@CustomerStreet2", sale.CustomerStreet2);
                parameters.Add("@CustomerCity", sale.CustomerCity);
                parameters.Add("@CustomerState", sale.CustomerState);
                parameters.Add("@CustomerZip", sale.CustomerZip);
                parameters.Add("@PurchasePrice", sale.PurchasePrice);
                parameters.Add("@PurchaseTypeID", sale.PurchaseTypeID);
                parameters.Add("@PurchaseDate", sale.PurchaseDate);
                parameters.Add("@VehicleID", sale.VehicleID);
                parameters.Add("@Id", sale.Id);

                cn.Execute("SalesInsert",
                    parameters, commandType: CommandType.StoredProcedure);

                sale.SalesID = parameters.Get<int>("@SalesID");
            }
        }
    }
}
