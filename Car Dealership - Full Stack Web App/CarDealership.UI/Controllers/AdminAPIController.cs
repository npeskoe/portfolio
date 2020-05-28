using CarDealership.Data.Factory;
using CarDealership.Models.Queries;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.UI.Controllers
{
    public class AdminAPIController : ApiController
    {
        [Route("vehicles/getmodels/{makeName}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetModelsByMakes(string makeName)
        {
            var repo = CarModelItemRepositoryFactory.GetRepository();

            try
            {
                return Ok(repo.GetModelsByMake(makeName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("vehicles/makes/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllMakes()
        {
            var repo = CarMakesRepositoryFactory.GetRepository();

            try
            {
                return Ok(repo.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("vehicles/models/")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllModels()
        {
            var repo = CarModelItemRepositoryFactory.GetRepository();

            try
            {
                return Ok(repo.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("vehicles/{vehicleID}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteVehicle(int vehicleID)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            VehicleInventory vehicle = repo.GetByID(vehicleID);

            if (vehicle == null)
            {
                return NotFound();
            }
            else
            {
                var filepath = "~/images/" + vehicle.ImageFileName;
                FileInfo file = new FileInfo(filepath);
                if (file.Exists)
                {
                    file.Delete();
                }
                repo.DeleteVehicle(vehicleID);
                return Ok();
            }
        }
        [Route("specials")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetAllSpecials()
        {
            var repo = SalesSpecialsRepositoryFactory.GetRepository();

            try
            {
                return Ok(repo.GetAllSpecials());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [Route("specials/{specialID}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult DeleteSpecial(int specialID)
        {
            var repo = SalesSpecialsRepositoryFactory.GetRepository();

            SalesSpecials special = repo.GetSpecialByID(specialID);

            if (special == null)
            {
                return NotFound();
            }
            else
            {
                repo.DeleteSpecial(specialID);
                return Ok();
            }
        }

        [Route("vehicleinventory/admin/{search}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchSalesInventory(string search, string minPrice, string maxPrice, string minYear, string maxYear)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            var salesVehicles = repo.GetVehicleItems().ToList();

            var results = new List<VehicleItem>();

            if (search != "0")
            {
                results = (from v in salesVehicles
                           where v.MakeName.ToUpper().Contains(search.ToUpper()) || v.ModelName.ToUpper().Contains(search.ToUpper()) || v.YearBuilt.ToString() == search
                           select v).ToList();

                if (results.Count == 0)
                {
                    return NotFound();
                }

            }
            else
            {
                results = salesVehicles;
            }

            int min = 0;
            Int32.TryParse(minPrice, out min);

            int max = 0;
            Int32.TryParse(maxPrice, out max);

            int minY = 0;
            Int32.TryParse(minYear, out minY);

            int maxY = 0;
            Int32.TryParse(maxYear, out maxY);

            if (max != 0)
            {

                var priceResults = (from vp in results
                                    where vp.SalesPrice >= min && vp.SalesPrice <= max
                                    select vp).ToList();

                results = priceResults;

            }

            if (maxY != 0)
            {
                var yearResults = (from vpy in results
                                   where vpy.YearBuilt >= minY && vpy.YearBuilt <= maxY
                                   select vpy).ToList();

                results = yearResults;
            }

            try
            {
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("admin/salesreport/search/{user}/{startDate}/{endDate}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchSalesReport(string user, string startDate, string endDate)
        {
            var repo = SalesInformationRepositoryFactory.GetRepository();

            var sales = repo.GetSales();

            var userRepo = UsersRepositoryFactory.GetRepository();

            var users = userRepo.GetAll();

            List<SalesReport> salesResults = new List<SalesReport>();

            List<SalesReport> results = sales.GroupBy(s => s.Id)
                                        .Select(us => new SalesReport
                                        {
                                            UserName = us.Select(x => x.UserName).FirstOrDefault(),
                                            TotalSales = us.Sum(x => x.PurchasePrice),
                                            TotalVehicles = us.Select(x => x.SalesID).Count(),
                                            PurchaseDate = us.Select(x => x.PurchaseDate).FirstOrDefault()
                                        }).ToList();

            var username = (from s in sales
                            where s.UserName.Contains(user)
                            select s.UserName).FirstOrDefault();

            if (user != "0")
            {

                salesResults = (from r in results
                                                  where username == r.UserName
                                                  select r).ToList();

                if (salesResults.Count == 0)
                {
                    return NotFound();
                }
            }
            else
            {
                salesResults = results;
            }
            if (startDate != "0")
            {
                var beginDate = DateTime.Parse(startDate);

                var correctDate = beginDate.AddDays(1);

                var bDateResults = (from r in results
                                    where r.PurchaseDate >= correctDate
                                    select r).ToList();

                salesResults = bDateResults;
            }
            if (endDate != "0")
            {
                var stopDate = DateTime.Parse(endDate);

                var cDate = stopDate.AddDays(1);

                var eDateResults = (from r in results
                                    where r.PurchaseDate <= cDate
                                    select r).ToList();

                salesResults = eDateResults;
            }
            try
            {
                return Ok(salesResults);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
