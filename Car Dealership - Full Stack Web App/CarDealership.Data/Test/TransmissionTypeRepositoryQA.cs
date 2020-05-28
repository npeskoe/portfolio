using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    class TransmissionTypeRepositoryQA : ITransmissionTypesRepository
    {
        public List<TransmissionTypes> GetAll()
        {
            List<TransmissionTypes> types = new List<TransmissionTypes>();

            types.Add(new TransmissionTypes { TransmissionID = 1, TransmissionTypeName = "Automatic" });
            types.Add(new TransmissionTypes { TransmissionID = 2, TransmissionTypeName = "Manual" });

            return types;
        }
    }
}
