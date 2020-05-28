using CarDealership.Data.Interfaces;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    public class CarModelItemRepositoryQA : ICarModelItemRepository
    {
        public List<CarModelItem> GetAll()
        {
            List<CarModelItem> models = new List<CarModelItem>();

            models.Add(new CarModelItem { MakeID = 1, MakeName = "Acura", ModelID = 1, ModelName = "MDX", DateAdded = DateTime.Today, Email = "admin@test.com" });
            models.Add(new CarModelItem { MakeID = 1, MakeName = "Acura", ModelID = 2, ModelName = "RDX", DateAdded = DateTime.Today, Email = "admin@test.com" });
            models.Add(new CarModelItem { MakeID = 2, MakeName = "Audi", ModelID = 3, ModelName = "A3", DateAdded = DateTime.Today, Email = "admin@test.com" });

            return models;
        }

        public List<CarModelItem> GetModelsByMake(string makeName)
        {
            List<CarModelItem> models = GetAll();

            var modeslByMake = models.Where(m => m.MakeName == makeName).ToList();

            return modeslByMake;

        }
    }
}
