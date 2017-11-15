using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReverseSpectre.Models
{
    public class EmploymentInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmploymentInformationId { get; set; }
        public byte SourceOfFunds { get; set; } //Salary,Business,Commission/Fees,Remittance,Others
        public string SourceOfFundsInfo { get; set; }

        public string Employer { get; set; }
        public string Position { get; set; }

        public byte FormOfBusiness { get; set; } //Sole Proprietor,Partnership,Corporation

        public string EmployerBusinessAddress { get; set; }
        public string ContactNo { get; set; }
        public string NatureOfJob { get; set; }
        public byte YrsInJob { get; set; }

        public bool IsOverseas { get; set; } //determine if OFW

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public DateTime DateTimeCreated { get; set; }
        public DateTime? DateTimeExpired { get; set; }
    }

    public class FinancialData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FinancialDataId { get; set; }

        public double MonthlyIncomeSalary { get; set; }
        public double Incentives { get; set; } // or Commissions
        public double OtherIncome { get; set; }

        public double SpouseMonthlyIncomeSalary { get; set; }
        public double SpouseIncentives { get; set; } // or Commissions
        public double SpouseOtherIncome { get; set; }

        public double LivingUtilities { get; set; }
        public double EducationalMedical { get; set; }
        public double Amortization { get; set; }

        public double MonthlyDisposableIncome()
        {
            double a = MonthlyIncomeSalary + Incentives + OtherIncome;
            double b = SpouseMonthlyIncomeSalary + SpouseIncentives + SpouseOtherIncome;
            double c = LivingUtilities + EducationalMedical + Amortization;

            return ((a+b)-c);
        }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}