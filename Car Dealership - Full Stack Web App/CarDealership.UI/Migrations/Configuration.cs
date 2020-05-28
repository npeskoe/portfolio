namespace CarDealership.UI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CarDealership.UI.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<GuildCarsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GuildCarsDbContext context)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleMgr = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            if (!roleMgr.RoleExists("admin"))
                roleMgr.Create(new ApplicationRole() { Name = "admin" });
            
            if (!roleMgr.RoleExists("sales"))
                roleMgr.Create(new ApplicationRole() { Name = "sales" });

            if (!roleMgr.RoleExists("disabled"))
                roleMgr.Create(new ApplicationRole() { Name = "disabled" });

            var user = new ApplicationUser()
            {
                UserName = "npeskoe@gmail.com",
                FirstName = "Nick",
                LastName = "Peskoe",
                Email = "npeskoe@gmail.com"
            };

            var salesUser = new ApplicationUser()
            {
                UserName = "mike.gordon@gmail.com",
                FirstName = "Mike",
                LastName = "Gordon",
                Email = "mike.gordon@gmail.com"
            };

            var disabledUser = new ApplicationUser()
            {
                UserName = "npeskoe@gmail.com",
                FirstName = "Nick",
                LastName = "Peskoe",
                Email = "npeskoe@gmail.com"
            };

            // create the user with the manager class
            userMgr.Create(user,"Testing123!");
            userMgr.Create(salesUser, "Salestesting!");
            userMgr.Create(disabledUser, "Disabled!");

            // add the user to the admin role
            userMgr.AddToRole(user.Id, "admin");
            userMgr.AddToRole(salesUser.Id, "sales");
            userMgr.Create(disabledUser, "disabled");
        }
    }
}
