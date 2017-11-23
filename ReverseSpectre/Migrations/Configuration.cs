namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ReverseSpectre.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    internal sealed class Configuration : DbMigrationsConfiguration<ReverseSpectre.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ReverseSpectre.Models.ApplicationDbContext context)
        {
            // Roles
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!context.Roles.Any(r => r.Name == "Client"))
            {
                var role = new IdentityRole { Name = "Client" };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "RelationshipManager"))
            {
                var role = new IdentityRole { Name = "RelationshipManager" };
                roleManager.Create(role);
            }

            //// Accounts
            //var userStore = new UserStore<ApplicationUser>(context);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            
        }
    }
}
