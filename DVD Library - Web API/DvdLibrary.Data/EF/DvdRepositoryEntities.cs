using DvdLibrary.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvdLibrary.Data.EF
{
    public class DvdRepositoryEntities : DbContext
    {
        public DvdRepositoryEntities()
               : base("DvdLibrary")
        {
        }
        public DbSet<DVD> Dvds { get; set; }
    }
}
