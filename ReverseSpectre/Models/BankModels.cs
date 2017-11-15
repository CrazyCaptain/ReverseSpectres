using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class Bank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BankId { get; set; }

        public string BranchName { get; set; }
        public bool IsDisabled { get; set; }
    }

    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }

        public byte LoanTerm { get; set; } // 6 months, 12 months, percentage, no idea yet
        public string PropertyAddress { get; set; }

        public byte TypeOfProperty { get; set; } // Condominium,House&Lot,ResidentialLot,Townhouse,Others
        public string TypeOfPropertyInfo { get; set; }

        public double ContractPrice { get; set; }

        public string Seller { get; set; } // or Broker name or Developer name
        
        public virtual Bank Bank { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual List<LoanRequirement> LoanRequirements { get; set; }
    }

    public class LoanRequirement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanRequirementId { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public int LoanId { get; set; }
        [ForeignKey("LoanId")]
        public virtual Loan Loan { get; set; }
    }

    public class RequirementComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequirementCommentId { get; set; }
        public string Body { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public virtual LoanRequirement LoanRequirement { get; set; }
        
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}