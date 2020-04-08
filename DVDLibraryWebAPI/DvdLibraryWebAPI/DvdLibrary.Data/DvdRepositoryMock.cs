using DvdLibrary.Data.Interfaces;
using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Data
{
    class DvdRepositoryMock : IDvdRepository
    {
        private List<DVD> _Dvds = new List<DVD>
        {
            new DVD
            { DvdID=0, Title="A Great Tale", ReleaseYear=2015, Director="Sam Jones",
            Rating="PG", Notes="This really is a great tale!"},
            new DVD
            { DvdID=1, Title="A Good Tale", ReleaseYear=2012, Director="Joe Smith",
            Rating="PG-13", Notes="This is a good tale!"},
            new DVD
            { DvdID=2, Title="A Super Tale", ReleaseYear=2015, Director="Sam Jones",
            Rating="PG", Notes="A great remake!"},
            new DVD
            { DvdID=3, Title="Just a Tale", ReleaseYear=2015, Director="Joe Baker",
            Rating="PG", Notes="It is a tale!"}
        };

        public List<DVD> GetAll()
        {
            return _Dvds;
        }

        public DVD Get(int dvdID)
        {
            return _Dvds.FirstOrDefault(d => d.DvdID == dvdID);
        }

        public void Add(DVD dvd)
        {
            dvd.DvdID = _Dvds.Max(d => d.DvdID) + 1;
            _Dvds.Add(dvd);
        }

        public void Edit(DVD dvd)
        {
            var found = _Dvds.FirstOrDefault(d => d.DvdID == dvd.DvdID);

            if (found != null)
                found = dvd;
        }

        public void Delete(int dvdID)
        {
            _Dvds.RemoveAll(d => d.DvdID == dvdID);
        }
    }
}
