using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    public class SalesSpecialsRepositoryQA : ISalesSpecialRepository
    {
        public List<SalesSpecials> GetAllSpecials()
        {
            List<SalesSpecials> specials = new List<SalesSpecials>();

            specials.Add(new SalesSpecials { SpecialID = 1, SpecialName = "Acura MDX Special", SpecialDescription = "2.9% AND $750 Customer Rebate on a New MDX!" });

            return specials;
        }

        public SalesSpecials GetSpecialByID(int specialID)
        {
            var repo = GetAllSpecials();

            var special = repo.Where(s => s.SpecialID == specialID).FirstOrDefault();

            return special;
        }

        public void DeleteSpecial(int specialID)
        {
            var repo = GetAllSpecials();

            var special = GetSpecialByID(specialID);

            repo.Remove(special);
        }

        public void InsertSpecial(SalesSpecials special)
        {
            SalesSpecials newSpecial = new SalesSpecials();

            var repo = GetAllSpecials();

            newSpecial.SpecialName = special.SpecialName;
            newSpecial.SpecialDescription = special.SpecialDescription;

            repo.Insert(special.SpecialID, newSpecial);

        }
    }
}
