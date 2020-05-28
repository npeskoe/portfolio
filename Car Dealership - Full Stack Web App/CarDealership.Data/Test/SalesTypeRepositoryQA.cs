using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    class SalesTypeRepositoryQA : ISalesTypeRepository
    {
        public List<SalesTypes> GetAll()
        {
            List<SalesTypes> types = new List<SalesTypes>();

            types.Add(new SalesTypes { SalesTypeID = 1, SalesTypeName = "New" });
            types.Add(new SalesTypes { SalesTypeID = 2, SalesTypeName = "Used" });

            return types;
        }
    }
}
