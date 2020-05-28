using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    public class CarModelsRepositoryQA : ICarModelsRepository
    {
        public List<CarModels> GetAll()
        {
            List<CarModels> models = new List<CarModels>();

            models.Add(new CarModels { ModelID = 1, MakeID = 1, ModelName = "MDX", DateAdded = DateTime.Today, Email = "admin@test.com" });
            models.Add(new CarModels { ModelID = 2, MakeID = 1, ModelName = "RDX", DateAdded = DateTime.Today, Email = "admin@test.com" });
            models.Add(new CarModels { ModelID = 3, MakeID = 3, ModelName = "A3", DateAdded = DateTime.Today, Email = "admin@test.com" });

            return models;
        }

        public void Insert(CarModels model)
        {
            CarModels newModel = new CarModels();

            var repo = GetAll();

            newModel.MakeID = model.MakeID;
            newModel.ModelName = model.ModelName;
            newModel.DateAdded = model.DateAdded;
            newModel.Email = model.Email;

            repo.Insert(newModel.ModelID, newModel);
            
        }
    }
}
