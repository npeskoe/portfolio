using DvdLibrary.Data;
using DvdLibrary.Data.Factories;
using DvdLibrary.Data.Interfaces;
using DvdLibrary.Models;
using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DvdLibraryWebAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DvdController : ApiController
    {
        IDvdRepository repository = DvdRepositoryFactory.GetRepository();

        [Route("dvds")]
        [AcceptVerbs("GET")]
        public IHttpActionResult All()
        {
            return Ok(repository.GetAll());
        }

        [Route("dvd/{dvdID}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetDVDs(int dvdID)
        {
            DVD dvd = repository.Get(dvdID);

            if (dvd == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvd);
            }
        }

        [Route("dvds/title/{title}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByTitle(string title)
        {
            List<DVD> dvds = repository.GetAll();

            var dvdTitle = dvds.Where(d => d.Title == title);

            if (dvdTitle == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvdTitle);
            }
        }
        [Route("dvds/year/{releaseYear}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByReleaseYear(int releaseYear)
        {
            List<DVD> dvds = repository.GetAll();

            var dvdReleaseYear = dvds.Where(d => d.ReleaseYear == releaseYear);

            if (dvdReleaseYear == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvdReleaseYear);
            }
        }
        [Route("dvds/director/{director}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByDirector(string director)
        {
            List<DVD> dvds = repository.GetAll();

            var dvdDirector = dvds.Where(d => d.Director == director);

            if (dvdDirector == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvdDirector);
            }
        }
        [Route("dvds/rating/{rating}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetByRating(string rating)
        {
            List<DVD> dvds = repository.GetAll();

            var dvdRating = dvds.Where(d => d.Rating == rating);

            if (dvdRating == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(dvdRating);
            }
        }
        [Route("dvd")]
        [AcceptVerbs("POST")]
        public IHttpActionResult Add(AddDvdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DVD dvd = new DVD()
            {
                Title = request.Title,
                ReleaseYear = request.ReleaseYear,
                Director = request.Director,
                Rating = request.Rating,
                Notes = request.Notes
            };

            repository.Add(dvd);
            return Created($"dvd/{dvd.DvdID}", dvd);
        }

        [Route("dvd/{dvdID}")]
        [AcceptVerbs("PUT")]
        public IHttpActionResult Update(UpdateDvdRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            DVD dvd = repository.Get(request.DvdID);

            if (dvd == null)
            {
                return NotFound();
            }

            dvd.Title = request.Title;
            dvd.ReleaseYear = request.ReleaseYear;
            dvd.Director = request.Director;
            dvd.Rating = request.Rating;
            dvd.Notes = request.Notes;

            repository.Edit(dvd);
            return Ok(dvd);
        }

        [Route("dvd/{dvdID}")]
        [AcceptVerbs("DELETE")]
        public IHttpActionResult Delete(int dvdID)
        {
            DVD dvd = repository.Get(dvdID);

            if (dvd == null)
            {
                return NotFound();
            }

            repository.Delete(dvdID);
            return Ok();
        }
    }
}