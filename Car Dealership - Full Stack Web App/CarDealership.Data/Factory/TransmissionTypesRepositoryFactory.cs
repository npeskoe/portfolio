using CarDealership.Data.Dapper;
using CarDealership.Data.Interfaces;
using CarDealership.Data.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Factory
{
    public class TransmissionTypesRepositoryFactory
    {
        public static ITransmissionTypesRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new TransmissionTypesRepositoryDapper();
                case "QA":
                    return new TransmissionTypeRepositoryQA();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
