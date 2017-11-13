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
    }

    public class Loan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }

        public byte LoanTerm { get; set; }
        public string PropertyAddress { get; set; }

        public byte TypeOfProperty { get; set; } //Condominium,House&Lot,ResidentialLot,Townhouse,Others
        public string TypeOfPropertyInfo { get; set; }

        public double ContractPrice { get; set; }

        public string Seller { get; set; } // or Broker or Developer
        
        public virtual Bank Bank { get; set; }
    }
}