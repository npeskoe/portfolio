using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    public class BodyTypesRepositoryQA : IBodyTypesRepository
    {
        public List<BodyTypes> GetAll()
        {
            List<BodyTypes> types = new List<BodyTypes>();

            types.Add(new BodyTypes { BodyTypeID = 1, BodyTypeName = "Car" });
            types.Add(new BodyTypes { BodyTypeID = 2, BodyTypeName = "SUV" });
            types.Add(new BodyTypes { BodyTypeID = 3, BodyTypeName = "Truck" });
            types.Add(new BodyTypes { BodyTypeID = 4, BodyTypeName = "Van" });

            return types;

        }
    }
}
