using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Bank
        public DbSet<Bank> Banks { get; set; }
        public DbSet<LoanType> LoanTypes { get; set; }
        public DbSet<Client> Clients { get; set; }

        // Loan
        public DbSet<LoanApplication> LoanApplication { get; set; }
        public DbSet<LoanApplicationDocument> LoanApplicationDocuments { get; set; }
        public DbSet<LoanApplicationDocumentFile> LoanApplicationDocumentFiles { get; set; }
        public DbSet<LoanApplicationPartner> LoanApplicationPartners { get; set; }
        public DbSet<LoanApplicationReference> LoanApplicationReferences { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ReverseSpectre.Models.RelationshipManager> RelationshipManagers { get; set; }
    }
}