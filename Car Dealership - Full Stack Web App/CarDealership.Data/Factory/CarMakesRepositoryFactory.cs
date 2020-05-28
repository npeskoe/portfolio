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
    public class CarMakesRepositoryFactory
    {
        public static ICarMakesRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new CarMakesRepositoryDapper();
                case "QA" :
                    return new CarMakesRepositoryQA();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
