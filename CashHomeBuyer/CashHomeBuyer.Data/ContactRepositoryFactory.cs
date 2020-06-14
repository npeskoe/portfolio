using CashHomeBuyer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashHomeBuyer.Data
{
    public static class ContactRepositoryFactory
    {
        public static IContactRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new ContactRepositoryDapper();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
