using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    public class ContactUsRepositoryQA : IContactUsRepository
    {
        public List<ContactUsRequest> GetAll()
        {
            List<ContactUsRequest> requests = new List<ContactUsRequest>();

            requests.Add(new ContactUsRequest { ContactUsID = 1, ContactName = "Bob Smith", ContactEmail = "bob@bobsmith.com", ContactPhone = "502-111-1111", ContactMessage = "I am interested in the used Acura MDX" });

            return requests;
        }

        public void Insert(ContactUsRequest request)
        {
            ContactUsRequest newRequest = new ContactUsRequest();

            var repo = GetAll();

            newRequest.ContactName = request.ContactName;
            newRequest.ContactEmail = request.ContactEmail;
            newRequest.ContactPhone = request.ContactPhone;
            newRequest.ContactMessage = request.ContactMessage;

            repo.Insert(newRequest.ContactUsID, newRequest);
        }
    }
}
