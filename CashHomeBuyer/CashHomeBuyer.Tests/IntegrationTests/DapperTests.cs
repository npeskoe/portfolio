using CashHomeBuyer.Data;
using CashHomeBuyer.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashHomeBuyer.Tests.IntegrationTests
{
    [TestFixture]
    public class DapperTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["MGAHomes"].ConnectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        [Test]
        public void CanLoadContacts()
        {
            var repo = new ContactRepositoryDapper();

            var contacts = repo.GetAll();

            Assert.AreEqual(3, contacts.Count);

            Assert.AreEqual(1, contacts[0].ContactID);
            Assert.AreEqual("Bob Smith", contacts[0].ContactName);
        }
        [Test]
        public void CanAddContact()
        {
            Contact contact = new Contact();

            var repo = new ContactRepositoryDapper();

            contact.ContactName = "Eric Williams";
            contact.ContactEmail = "eric.williams@gmail.com";
            contact.ContactPhone = "8124356789";
            var dateTime = DateTime.Parse("06/03/2020");
            contact.ContactDate = dateTime;
            contact.Notes = null;

            repo.InsertContact(contact);

            var loaded = repo.GetByID(4);

            Assert.IsNotNull(loaded);

            repo.DeleteContact(4);

            loaded = repo.GetByID(4);

            Assert.IsNull(loaded);
           
        }
        [Test]
        public void CanAddNotes()
        {
            var repo = new ContactRepositoryDapper();

            var contact = repo.GetByID(1);

            contact.Notes = "This is a good lead";

            repo.AddNote(contact);

            var contacts = repo.GetAll();

            Assert.IsNotNull(contacts[0].Notes);
            Assert.AreEqual(contacts[0].Notes, "This is a good lead");
        }
    }
}
