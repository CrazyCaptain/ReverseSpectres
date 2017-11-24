using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;

namespace ReverseSpectre.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("UserId", this.Id));

            return userIdentity;
        }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Bank
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BusinessManager> BusinessManagers { get; set; }
        public DbSet<RelationshipManager> RelationshipManagers { get; set; }
        public DbSet<AccountingOfficer> AccountingOfficers { get; set; }

        // Client
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientInvitation> ClientInvitations { get; set; }

        public DbSet<TwoFactorAuthSMS> TwoFactorAuthSMS { get; set; }
        public DbSet<ContactInformation> EmploymentInformations { get; set; }

        // Loan
        public DbSet<LoanApplication> LoanApplication { get; set; }
        public DbSet<LoanApplicationDocument> LoanApplicationDocuments { get; set; }
        public DbSet<LoanApplicationDocumentFile> LoanApplicationDocumentFiles { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
}