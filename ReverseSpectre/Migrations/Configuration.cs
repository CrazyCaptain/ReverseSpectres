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

            if (!context.Roles.Any(r => r.Name == "SalesDirector"))
            {
                var role = new IdentityRole { Name = "SalesDirector" };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "AccountingOfficer"))
            {
                var role = new IdentityRole { Name = "AccountingOfficer" };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "CommercialBank"))
            {
                var role = new IdentityRole { Name = "CommercialBank" };
                roleManager.Create(role);
            }

            // Bank
            if (!context.Banks.Any(b => b.BankId == 1))
            {
                context.Banks.AddOrUpdate(new Bank()
                {
                    BankId = 1,
                    BranchName = "Taft",
                });
            }

            // Accounts
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // BusinessManager
            if (!context.Users.Any(u => u.UserName == "reversehack_bm@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "reversehack_bm@mailinator.com",
                    Email = "reversehack_bm@mailinator.com",
                    EmailConfirmed = true,
                    FirstName = "First",
                    MiddleName = "Middle",
                    LastName = "Last",
                };
                userManager.Create(user, "Testing@123");
                userManager.AddToRole(user.Id, "Client");

                context.BusinessManagers.AddOrUpdate(new BusinessManager()
                {
                    BusinessManagerId = 1,
                    TimestampCreated = DateTime.Now,
                    BankId = 1,
                    UserId = user.Id
                });
            }

            // Relationship Manager
            if (!context.Users.Any(u => u.UserName == "reversehack_rm@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "reversehack_rm@mailinator.com",
                    Email = "reversehack_rm@mailinator.com",
                    EmailConfirmed = true,
                    FirstName = "First",
                    MiddleName = "Middle",
                    LastName = "Last",
                };
                userManager.Create(user, "Testing@123");
                userManager.AddToRole(user.Id, "RelationshipManager");

                context.RelationshipManagers.Add(new RelationshipManager()
                {
                    RelationshipManagerId = 1,
                    IsDisabled = false,
                    TimestampCreated = DateTime.Now,
                    BusinessManagerId = 1,
                    UserId = user.Id
                });
            }

            // Account Officer
            if (!context.Users.Any(u => u.UserName == "reversehack_ao@mailinator.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "reversehack_ao@mailinator.com",
                    Email = "reversehack_ao@mailinator.com",
                    EmailConfirmed = true,
                    FirstName = "First",
                    MiddleName = "Middle",
                    LastName = "Last",
                };
                userManager.Create(user, "Testing@123");
                userManager.AddToRole(user.Id, "AccountingOfficer");

                context.AccountingOfficers.Add(new AccountingOfficer()
                {
                    AccountingOfficerId = 1,
                    IsDisabled = false,
                    TimestampCreated = DateTime.Now,
                    UserId = user.Id
                });
            }
        }
    }
}
