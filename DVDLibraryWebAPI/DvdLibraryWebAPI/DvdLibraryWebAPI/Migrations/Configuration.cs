namespace DvdLibraryWebAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DvdLibrary.Data.EF;
    using DvdLibrary.Models.Tables;
    using DvdLibraryWebAPI.Models;
    using static DvdLibrary.Data.DvdRepositoryEF;

    internal sealed class Configuration : DbMigrationsConfiguration<DvdRepositoryEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DvdRepositoryEntities context)
        {
            context.Dvds.AddOrUpdate(
                d => d.Title,
                new DVD {
                    Title = "A Great Tale",
                    ReleaseYear = 2014,
                    Director = "John Jackson",
                    Rating = "G",
                    Notes = "Family, Friendly, Fun!"
                });
        }
    }
}
