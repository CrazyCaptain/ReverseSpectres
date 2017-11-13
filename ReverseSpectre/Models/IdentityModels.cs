using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace ReverseSpectre.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public byte Gender { get; set; }

        public string PlaceOfBirth { get; set; }
        public DateTime DateOfBirth { get; set; }

        public byte Nationality { get; set; }
        public string NationalityInfo { get; set; }

        public byte CivilStatus { get; set; }

        public string TIN { get; set; }
        public string SSS { get; set; }

        public string HomeAddress { get; set; }
        public byte Ownership { get; set; }
        public string LengthOfStay { get; set; }

        public string PermanentHomeAddress { get; set; }
        public string HomePhoneNo { get; set; }
        public virtual List<MobileNumber> MobileNumbers { get; set; }
        public string OfficePhoneNo { get; set; }

        public byte NumOfDependents { get; set; }
        public string AgesOfDependents { get; set; }

        public virtual List<EmploymentInformation> EmploymentInfo { get; set; }
        
        public virtual Bank Bank { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<TwoFactorAuth> TwoFactorAuths { get; set; }
        public DbSet<EmploymentInformation> EmploymentInformations { get; set; }
        public DbSet<FinancialData> FinancialData { get; set; }
        public DbSet<MobileNumber> MobileNumbers { get; set; }
        public DbSet<Message> Messages { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Loan>().HasOptional(l => l.Bank);
            modelBuilder.Entity<ApplicationUser>().HasOptional(l => l.Bank);
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}