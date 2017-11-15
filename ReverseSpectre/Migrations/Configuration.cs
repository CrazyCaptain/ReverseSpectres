namespace ReverseSpectre.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ReverseSpectre.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ReverseSpectre.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            /*seed roles*/
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            if (!context.Roles.Any(r => r.Name == "ComBank"))
            {
                var role = new IdentityRole { Name = "ComBank" };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "RM"))
            {
                var role = new IdentityRole { Name = "RM" };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "SD"))
            {
                var role = new IdentityRole { Name = "SD" };
                roleManager.Create(role);
            }

            /*seed users*/
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager)
            {
                AllowOnlyAlphanumericUserNames = false,
            };

            context.Banks.AddOrUpdate(
                b => b.BranchName,
                new Bank { BranchName = "Manila - Taft", IsDisabled = false },
                new Bank { BranchName = "Mandaluyong - Boni", IsDisabled = false },
                new Bank { BranchName = "Makati - Buendia", IsDisabled = false },
                new Bank { BranchName = "San Juan - Green Hills", IsDisabled = false }
            );

            if (!context.Users.Any(u => u.UserName == "rm@gmail.com"))
            {
                var user = new ApplicationUser
                {
                    FirstName = "Relationship Manager A",
                    MiddleName = "",
                    LastName = "Test",
                    UserName = "rm@gmail.com",
                    Email = "rm@gmail.com",
                    EmailConfirmed = true,
                    IsDisabled = false,
                    DateOfBirth = DateTime.UtcNow.AddHours(8),
                    Bank = context.Banks.FirstOrDefault(b=>b.BranchName=="Manila - Taft")
                };
                userManager.Create(user, "Testing@123");
                userManager.AddToRole(user.Id, "RM");
            }
        }
    }
}
