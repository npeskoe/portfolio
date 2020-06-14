using CashHomeBuyer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashHomeBuyer.Data
{
    public class ContactRepositoryDapper : IContactRepository
    {
        public List<Contact> GetAll()
        {
            List<Contact> contacts = new List<Contact>();

            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                contacts = cn.Query<Contact>("ContactRecordsSelectAll", commandType: CommandType.StoredProcedure).ToList();
            }

            return contacts;
        }

        public void DeleteContact(int contactID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ContactID", contactID);

                cn.Execute("ContactInfoDelete", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public void InsertContact(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@ContactID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@ContactAddress", contact.ContactAddress);
                parameters.Add("@ContactName", contact.ContactName);
                parameters.Add("@ContactEmail", contact.ContactEmail);
                parameters.Add("@ContactPhone", contact.ContactPhone);
                parameters.Add("@ContactDate", contact.ContactDate);
                parameters.Add("@Notes", contact.Notes);

                cn.Execute("ContactRecordInsert", parameters, commandType: CommandType.StoredProcedure);

                contact.ContactID = parameters.Get<int>("@ContactID");
            }
        }
        public void EditContact(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@ContactID", contact.ContactID);
                parameters.Add("@ContactAddress", contact.ContactAddress);
                parameters.Add("@ContactName", contact.ContactName);
                parameters.Add("@ContactEmail", contact.ContactEmail);
                parameters.Add("@ContactPhone", contact.ContactPhone);
                parameters.Add("@ContactDate", contact.ContactDate);
                parameters.Add("@Notes", contact.Notes);

                cn.Execute("ContactUpdate",
                    parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public Contact GetByID(int contactID)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ContactID", contactID);

                return cn.Query<Contact>(
                    "ContactSelect",
                    parameters,
                    commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void AddNote(Contact contact)
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ContactID", contact.ContactID);
                parameters.Add("@Notes", contact.Notes);

                cn.Execute("AddNotes",
                   parameters,
                   commandType: CommandType.StoredProcedure);
            }
        }
    }
}
