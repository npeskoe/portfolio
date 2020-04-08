using DvdLibrary.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DvdLibrary.Models.Tables;
using DvdLibrary.Data.EF;

namespace DvdLibrary.Data
{
    public class DvdRepositoryEF : IDvdRepository
    {
        DvdRepositoryEntities repository = new DvdRepositoryEntities();

        public List<DVD> GetAll()
        {

            var dvds = from dvd in repository.Dvds
                       select dvd;

            var dvdList = dvds.ToList();

            return dvdList;
        }

        public DVD Get(int dvdID)
        {
            var dvd = repository.Dvds.FirstOrDefault(d => d.DvdID == dvdID);

            return dvd;
        }

        public void Add(DVD dvd)
        {
            DVD movie = new DVD();
            movie.Title = dvd.Title;
            movie.ReleaseYear = dvd.ReleaseYear;
            movie.Director = dvd.Director;
            movie.Rating = dvd.Rating;
            movie.Notes = dvd.Notes;

            repository.Dvds.Add(movie);
            repository.SaveChanges();
        }

        public void Edit(DVD dvd)
        {
            DVD Dvd = new DVD();

            Dvd.DvdID = dvd.DvdID;
            Dvd.Title = dvd.Title;
            Dvd.ReleaseYear = dvd.ReleaseYear;
            Dvd.Director = dvd.Director;
            Dvd.Rating = dvd.Rating;
            Dvd.Notes = dvd.Notes;

            repository.Entry(dvd).State = EntityState.Modified;
            repository.SaveChanges();

        }

        public void Delete(int dvdID)
        {
            var dvd = repository.Dvds.FirstOrDefault(d => d.DvdID == dvdID);

            if (dvd != null)
            {
                repository.Dvds.Remove(dvd);
                repository.SaveChanges();
            }
        }
    }
    
}
