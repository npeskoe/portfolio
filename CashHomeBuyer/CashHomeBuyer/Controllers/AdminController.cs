using CashHomeBuyer.Data;
using CashHomeBuyer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CashHomeBuyer.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult ManageContacts()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EditContact(int id)
        {
            var repo = ContactRepositoryFactory.GetRepository();

            Contact editContact = new Contact();

            var contact = repo.GetByID(id);

            editContact.ContactID = contact.ContactID;

            editContact.ContactAddress = contact.ContactAddress;

            editContact.ContactName = contact.ContactName;

            editContact.ContactEmail = contact.ContactEmail;

            editContact.ContactPhone = contact.ContactPhone;

            editContact.ContactDate = contact.ContactDate;

            editContact.Notes = contact.Notes;

            return View(editContact);
        }

        [HttpPost]
        public ActionResult EditContact(Contact contact)
        {
            Contact newContact = new Contact();

            var repo = ContactRepositoryFactory.GetRepository();

            newContact.ContactID = contact.ContactID;
            newContact.ContactAddress = contact.ContactAddress;
            newContact.ContactName = contact.ContactName;
            newContact.ContactEmail = contact.ContactEmail;
            newContact.ContactPhone = contact.ContactPhone;
            newContact.ContactDate = contact.ContactDate;
            newContact.Notes = contact.Notes;

            if (ModelState.IsValid)
            {

                repo.EditContact(newContact);

                return RedirectToAction("ManageContacts");

            }
            else
            {
                return View("EditContact", newContact);
            }
        }
    }
}