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
    public class UsersRepositoryDapper : IUsersRepository
    {
        public List<Users> GetAll()
        {
            List<Users> users = new List<Users>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                users = cn.Query<Users>("UsersSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return users;
        }

        public List<UserRoles> GetUserRoles()
        {
            List<UserRoles> roles = new List<UserRoles>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                roles = cn.Query<UserRoles>("RolesSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return roles;
        }

    }
}
