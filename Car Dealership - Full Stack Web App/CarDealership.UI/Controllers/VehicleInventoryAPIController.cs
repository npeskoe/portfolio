using CarDealership.Data.Factory;
using CarDealership.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.UI.Controllers
{
    public class VehicleInventoryAPIController : ApiController
    {
        [Route("vehicleinventory/new")]
        [AcceptVerbs("GET")]
        public IHttpActionResult New()
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                return Ok(repo.GetNewInventory());
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("vehicleinventory/used")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Used()
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            try
            {
                return Ok(repo.GetUsedInventory());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("vehicleinventory/new/{search}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchNewInventory(string search, string minPrice, string maxPrice, string minYear, string maxYear)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            var newVehicles = repo.GetNewInventory().ToList();

            var availableVehicles = repo.GetVehicleItems().ToList();

            var results = new List<VehicleItem>();

            

            if (search != "0")
            {
                results = (from v in newVehicles
                               where v.MakeName.ToUpper().Contains(search.ToUpper()) || v.ModelName.ToUpper().Contains(search.ToUpper()) || v.YearBuilt.ToString() == search
                               select v).ToList();

                if (results.Count == 0)
                {
                    return NotFound();
                }
                
            }
            else
            {
                results = newVehicles;
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

        [Route("vehicleinventory/used/{search}/{minPrice}/{maxPrice}/{minYear}/{maxYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult SearchUsedInventory(string search, string minPrice, string maxPrice, string minYear, string maxYear)
        {
            var repo = VehicleRepositoryFactory.GetRepository();

            var usedVehicles = repo.GetUsedInventory().ToList();

            var availableVehicles = repo.GetVehicleItems().ToList();

            var results = new List<VehicleItem>();

            foreach (var u in usedVehicles)
            {
                u.IsSold = availableVehicles.Any(v => v.VehicleID == u.VehicleID);
            }

            if (search != "0")
            {
                results = (from v in usedVehicles
                           where v.MakeName.ToUpper().Contains(search.ToUpper()) || v.ModelName.ToUpper().Contains(search.ToUpper()) || v.YearBuilt.ToString() == search
                           select v).ToList();

                if (results.Count == 0)
                {
                    return NotFound();
                }

            }
            else
            {
                results = usedVehicles;
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
    }
}