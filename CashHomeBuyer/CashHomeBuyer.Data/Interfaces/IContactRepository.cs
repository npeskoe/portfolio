using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashHomeBuyer.Models
{
    public interface IContactRepository
    {
        List<Contact> GetAll();
        void DeleteContact(int contactID);
        void InsertContact(Contact contact);
        void EditContact(Contact contact);
        Contact GetByID(int contactID);
        void AddNote(Contact contact);
    }
}
