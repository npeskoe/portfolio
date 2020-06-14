using CashHomeBuyer.Data;
using CashHomeBuyer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using Twilio.Types;

namespace CashHomeBuyer.Controllers
{
    public class HomeController : TwilioController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertContact(Contact contact)
        {

            var repo = ContactRepositoryFactory.GetRepository();

            DateTime today = DateTime.Today;

            contact.ContactDate = today;

            if (ModelState.IsValid)
            {
                repo.InsertContact(contact);

                var message = new MailMessage();
                message.To.Add(new MailAddress("jimmy@jimmywelch.com", "Jimmy Welch"));
                message.From = new MailAddress("jimmy@jimmywelch.com");
                message.Subject = "New Form Submission from BuyMyLouisvilleHome.com";
                message.Body = "New Contact:" + Environment.NewLine +
                    "Address: " + contact.ContactAddress + Environment.NewLine +
                    "Name: " + contact.ContactName + Environment.NewLine +
                    "Email: " + contact.ContactEmail + Environment.NewLine +
                    "Phone: " + contact.ContactPhone; 

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = ConfigurationManager.AppSettings["EmailUserName"],
                        Password = ConfigurationManager.AppSettings["EmailPassword"]
                    };
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }

                var accountSid = ConfigurationManager.AppSettings["TwilioAccountSid"];
                var authToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
                TwilioClient.Init(accountSid, authToken);

                var to = new PhoneNumber(ConfigurationManager.AppSettings["MyPhoneNumber"]);
                var from = new PhoneNumber("+15024307659");

                var smsMessage = MessageResource.Create(
                    to: to,
                    from: from,
                    body: "You have a new Contact Form Submission on WeBuyHomesinLouisville.com: " + Environment.NewLine +
                    "Address: " + contact.ContactAddress + Environment.NewLine +
                    "Name: " + contact.ContactName + Environment.NewLine +
                    "Email: " + contact.ContactEmail + Environment.NewLine +
                    "Phone: " + contact.ContactPhone);


                return RedirectToAction("FormSubmission");
            }

            return View("Index", contact);
        }

        public ActionResult FormSubmission()
        {
            return View();
        }
        

        public ActionResult About()
        { 
            return View();
        }

        public ActionResult OurProcess()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}