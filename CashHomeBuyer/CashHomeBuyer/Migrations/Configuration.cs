namespace CashHomeBuyer.Migrations
{
    using CashHomeBuyer.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CashHomeBuyerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CashHomeBuyerDbContext context)
        {
            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleMgr = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            if (!roleMgr.RoleExists("admin"))
                roleMgr.Create(new ApplicationRole() { Name = "admin" });

            var user = new ApplicationUser()
            {
                UserName = "jimmy@jimmywelch.com",
                Email = "jimmy@jimmywelch.com"
            };

            userMgr.Create(user, "$Tick123");
        }
    }
}
