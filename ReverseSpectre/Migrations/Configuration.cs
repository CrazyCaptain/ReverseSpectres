namespace ReverseSpectre.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ReverseSpectre.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ReverseSpectre.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReverseSpectre.Models.ApplicationDbContext context)
        {
            context.LoanTypes.AddOrUpdate(new LoanType()
            {
                LoanTypeId = 1,
                InterestRate = 0.05,
                Name = "Home Loan"
            });
        }
    }
}
