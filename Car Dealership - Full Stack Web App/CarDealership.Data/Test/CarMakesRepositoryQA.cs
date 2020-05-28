using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Test
{
    public class CarMakesRepositoryQA : ICarMakesRepository
    {
        public List<CarMakes> GetAll()
        {
            List<CarMakes> makes = new List<CarMakes>();

            makes.Add(new CarMakes { MakeID = 1, MakeName = "Acura", DateAdded = DateTime.Today, Email = "admin@test.com" });
            makes.Add(new CarMakes { MakeID = 2, MakeName = "Audi", DateAdded = DateTime.Today, Email = "admin@test.com" });

            return makes;

        }

        public void Insert(CarMakes make)
        {
            var repo = GetAll();

            CarMakes carMake = new CarMakes();

            carMake.MakeName = make.MakeName;
            carMake.DateAdded = make.DateAdded;
            carMake.Email = make.Email;

            repo.Insert(carMake.MakeID, carMake);

        }
    }
}
