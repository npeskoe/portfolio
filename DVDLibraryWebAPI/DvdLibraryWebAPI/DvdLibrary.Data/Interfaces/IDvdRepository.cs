using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Data.Interfaces
{
    public interface IDvdRepository
    {
        List<DVD> GetAll();

        DVD Get(int dvdID);

        void Add(DVD dvd);

        void Edit(DVD dvd);

        void Delete(int dvdID);
    }
}
