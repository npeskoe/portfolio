using CashHomeBuyer.Data;
using CashHomeBuyer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CashHomeBuyer.Controllers
{
    public class AdminAPIController : ApiController
    {
        [Route("contacts")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllContacts()
        {
            var repo = ContactRepositoryFactory.GetRepository();

            try
            {
                return Ok(repo.GetAll());
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("contacts/delete/{contactID}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteContact(int contactID)
        {
            var repo = ContactRepositoryFactory.GetRepository();

            Contact contact = repo.GetByID(contactID);

            if (contact == null)
            {
                return NotFound();
            }
            else
            {
                repo.DeleteContact(contactID);
                return Ok();
            } 

        }
    }
}
