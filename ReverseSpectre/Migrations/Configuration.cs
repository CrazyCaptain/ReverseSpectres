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

            // Accounts
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            if (!context.Users.Any(u => u.UserName == "jackrobertfrost@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "jackrobertfrost@mailinator.com",
                    Email = "jackrobertfrost@mailinator.com",
                    EmailConfirmed = true,
                };
                userManager.Create(user, "Testing@123");
                userManager.AddToRole(user.Id, "Client");

                context.Clients.Add(new Client()
                {
                    FirstName = "Jack",
                    MiddleName = "Robert",
                    LastName = "Masahud",
                    Birthdate = new DateTime(1990, 1, 1),
                    Nationality = "Philippines",
                    UserId = user.Id
                });
            }
            else
            {
                var user = userManager.FindByEmail("jackrobertfrost@mailinator.com");
                userManager.AddToRole(user.Id, "Client");
            }

            // Misc
            context.LoanTypes.AddOrUpdate(new LoanType()
            {
                LoanTypeId = 1,
                InterestRate = 0.05,
                Name = "Home Loan"
            });


            
        }
    }
}
